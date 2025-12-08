using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Team24_BevosBooks.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Team24_BevosBooks.DAL;
using Microsoft.AspNetCore.Authorization;

namespace Team24_BevosBooks.Controllers
{
    public class ProcurementController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ProcurementController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ==============================
        // MANUAL REORDER
        // ==============================
        [Authorize]
        public async Task<IActionResult> ManualReorder(string? searchString, int? genreId, bool inStockOnly = false, string sortOrder = "title")
        {
            if (!User.IsInRole("Admin"))
                return View("AccessDenied");
            
            IQueryable<Book> query = _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Reviews)
                .Where(b => b.BookStatus == "Active");

            if (!string.IsNullOrEmpty(searchString))
            {
                var keywords = searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var keyword in keywords)
                {
                    query = query.Where(b =>
                        b.Title.Contains(keyword) ||
                        b.Authors.Contains(keyword) ||
                        b.Genre.GenreName.Contains(keyword) ||
                        b.BookNumber.ToString().Contains(keyword));
                }
            }

            if (genreId.HasValue && genreId.Value != 0)
                query = query.Where(b => b.GenreID == genreId.Value);

            if (inStockOnly)
                query = query.Where(b => b.InventoryQuantity > 0);

            query = sortOrder switch
            {
                "author" => query.OrderBy(b => b.Authors),
                "newest" => query.OrderByDescending(b => b.PublishDate),
                "oldest" => query.OrderBy(b => b.PublishDate),
                "priceAsc" => query.OrderBy(b => b.Price),
                "priceDesc" => query.OrderByDescending(b => b.Price),
                "rating" => query.OrderByDescending(b =>
                    b.Reviews.Any(r => r.DisputeStatus == "Approved")
                        ? b.Reviews.Where(r => r.DisputeStatus == "Approved").Average(r => r.Rating)
                        : 0),
                "popularity" => query.OrderByDescending(b =>
                    _context.OrderDetails
                        .Where(od => od.BookID == b.BookID && od.Order.OrderStatus == "Ordered")
                        .Sum(od => (int?)od.Quantity) ?? 0),
                _ => query.OrderBy(b => b.Title),
            };

            var books = await query.ToListAsync();

            ViewBag.GenreID = new SelectList(await _context.Genres.OrderBy(g => g.GenreName).ToListAsync(), "GenreID", "GenreName");
            ViewBag.SelectedGenreId = genreId ?? 0;
            ViewBag.SearchString = searchString;
            ViewBag.InStockOnly = inStockOnly;
            ViewBag.SortOrder = sortOrder;

            // Load all books for fallback cost
            var bookCosts = await _context.Books
                .Select(b => new { b.BookID, b.Cost })
                .ToDictionaryAsync(b => b.BookID, b => b.Cost);

            // Load historical supplier costs
            var historicalCosts = await _context.OrderDetails
                .Where(od => od.Order.OrderStatus == "SupplierOrder" || od.Order.OrderStatus == "Received")
                .GroupBy(od => od.BookID)
                .Select(g => new {
                    BookID = g.Key,
                    LastCost = g.OrderByDescending(x => x.Order.OrderDate)
                                .Select(x => x.Cost)
                                .FirstOrDefault()
                })
                .ToDictionaryAsync(x => x.BookID, x => x.LastCost);

            // Final merged dictionary
            var lastCosts = new Dictionary<int, decimal>();

            foreach (var book in bookCosts)
            {
                if (historicalCosts.ContainsKey(book.Key) && historicalCosts[book.Key] > 0)
                {
                    lastCosts[book.Key] = historicalCosts[book.Key]; // Use real supplier order cost
                }
                else
                {
                    lastCosts[book.Key] = book.Value; // Use seeded Book.Cost
                }
            }

            ViewBag.LastCosts = lastCosts;


            var avgMargins = await _context.OrderDetails
                .Where(od => od.Order.OrderStatus == "Ordered")
                .GroupBy(od => od.BookID)
                .Select(g => new { BookID = g.Key, AvgMargin = g.Average(x => (x.Price - x.Cost) / x.Price) })
                .ToDictionaryAsync(x => x.BookID, x => x.AvgMargin);

            ViewBag.LastCosts = lastCosts;
            ViewBag.AvgMargins = avgMargins;

            return View(books);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ManualReorder(List<int> bookIds)
        {
            if (!User.IsInRole("Admin"))
                return View("AccessDenied");
            
            if (bookIds == null || bookIds.Count == 0)
                return RedirectToAction("ManualReorder");

            var adminId = _userManager.GetUserId(User);

            foreach (var id in bookIds)
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null) continue;

                if (!int.TryParse(Request.Form[$"qty_{id}"], out var qty)) continue;
                if (!decimal.TryParse(Request.Form[$"cost_{id}"], out var cost)) continue;
                if (qty < 0 || cost <= 0) continue;

                var order = new Order
                {
                    UserID = adminId,
                    OrderDate = DateTime.Now,
                    OrderStatus = "SupplierOrder",
                    ShippingFee = 0
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                var detail = new OrderDetail
                {
                    OrderID = order.OrderID,
                    BookID = book.BookID,
                    Quantity = qty,
                    Price = book.Price,
                    Cost = cost
                };
                _context.OrderDetails.Add(detail);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("ViewOrders");
        }

        // ==============================
        // AUTO REORDER
        // ==============================

        [Authorize]
        public async Task<IActionResult> AutoReorder()
        {
            if (!User.IsInRole("Admin"))
                return View("AccessDenied");
            
            var books = await _context.Books.Where(b => b.BookStatus == "Active").ToListAsync();

            var pendingOrders = await _context.OrderDetails
                .Include(od => od.Order)
                .Where(od => od.Order.OrderStatus == "SupplierOrder")
                .GroupBy(od => od.BookID)
                .Select(g => new { BookID = g.Key, PendingQty = g.Sum(x => x.Quantity) })
                .ToListAsync();

            var result = books.Where(b =>
                b.InventoryQuantity + (pendingOrders.FirstOrDefault(p => p.BookID == b.BookID)?.PendingQty ?? 0)
                < b.ReorderPoint).ToList();

            // Load all books for fallback cost
            var bookCosts = await _context.Books
                .Select(b => new { b.BookID, b.Cost })
                .ToDictionaryAsync(b => b.BookID, b => b.Cost);

            // Load historical supplier costs
            var historicalCosts = await _context.OrderDetails
                .Where(od => od.Order.OrderStatus == "SupplierOrder" || od.Order.OrderStatus == "Received")
                .GroupBy(od => od.BookID)
                .Select(g => new {
                    BookID = g.Key,
                    LastCost = g.OrderByDescending(x => x.Order.OrderDate)
                                .Select(x => x.Cost)
                                .FirstOrDefault()
                })
                .ToDictionaryAsync(x => x.BookID, x => x.LastCost);

            // Final merged dictionary
            var lastCosts = new Dictionary<int, decimal>();
            foreach (var book in bookCosts)
            {
                if (historicalCosts.ContainsKey(book.Key) && historicalCosts[book.Key] > 0)
                {
                    lastCosts[book.Key] = historicalCosts[book.Key]; // Use real supplier order cost
                }
                else
                {
                    lastCosts[book.Key] = book.Value; // Use seeded Book.Cost
                }
            }

            var avgMargins = await _context.OrderDetails
                .Where(od => od.Order.OrderStatus == "Ordered")
                .GroupBy(od => od.BookID)
                .Select(g => new { BookID = g.Key, AvgMargin = g.Average(x => (x.Price - x.Cost) / x.Price) })
                .ToDictionaryAsync(x => x.BookID, x => x.AvgMargin);

            ViewBag.LastCosts = lastCosts;
            ViewBag.AvgMargins = avgMargins;

            return View(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AutoReorder(List<int> bookIds)
        {
            if (!User.IsInRole("Admin"))
                return View("AccessDenied");
            
            if (bookIds == null || bookIds.Count == 0)
                return RedirectToAction("AutoReorder");

            var adminId = _userManager.GetUserId(User);

            foreach (var id in bookIds)
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null) continue;

                if (!int.TryParse(Request.Form[$"qty_{id}"], out var qty)) continue;
                if (!decimal.TryParse(Request.Form[$"cost_{id}"], out var cost)) continue;
                if (qty < 0 || cost <= 0) continue;

                var order = new Order
                {
                    UserID = adminId,
                    OrderDate = DateTime.Now,
                    OrderStatus = "SupplierOrder",
                    ShippingFee = 0
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                var detail = new OrderDetail
                {
                    OrderID = order.OrderID,
                    BookID = book.BookID,
                    Quantity = qty,
                    Price = book.Price,
                    Cost = cost
                };
                _context.OrderDetails.Add(detail);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("ViewOrders");
        }

        // ==============================
        // VIEW ORDERS
        // ==============================
        [Authorize]
        public async Task<IActionResult> ViewOrders()
        {
            if (!User.IsInRole("Admin"))
                return View("AccessDenied");


            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                .Where(o => o.OrderStatus == "SupplierOrder")
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            // Load dictionary from TempData (deserialize JSON string)
            var receivedTotals = TempData["ReceivedTotals"] != null
                ? JsonSerializer.Deserialize<Dictionary<int, int>>(TempData["ReceivedTotals"].ToString())
                : new Dictionary<int, int>();

            // Keep dictionary available for next request
            TempData.Keep("ReceivedTotals");

            ViewBag.ReceivedTotals = receivedTotals;

            return View(orders);
        }

        // ==============================
        // CHECK IN ARRIVALS
        // ==============================
        [HttpPost]
        public async Task<IActionResult> CheckIn(int orderDetailId, int arrivedQty)
        {
            var detail = await _context.OrderDetails
                .Include(od => od.Book)
                .Include(od => od.Order)
                .FirstOrDefaultAsync(od => od.OrderDetailID == orderDetailId);

            if (detail == null) return NotFound();

            if (arrivedQty < 0) arrivedQty = 0;

            // Load dictionary from TempData (deserialize JSON string)
            var receivedTotals = TempData["ReceivedTotals"] != null
                ? JsonSerializer.Deserialize<Dictionary<int, int>>(TempData["ReceivedTotals"].ToString())
                : new Dictionary<int, int>();

            int alreadyReceived = receivedTotals.ContainsKey(orderDetailId) ? receivedTotals[orderDetailId] : 0;

            // Clamp so we never exceed ordered quantity
            if (alreadyReceived + arrivedQty > detail.Quantity)
            {
                arrivedQty = detail.Quantity - alreadyReceived;
            }

            // Update dictionary
            receivedTotals[orderDetailId] = alreadyReceived + arrivedQty;

            // Save dictionary back to TempData (serialize to JSON string)
            TempData["ReceivedTotals"] = JsonSerializer.Serialize(receivedTotals);

            // Update inventory
            detail.Book.InventoryQuantity += arrivedQty;

            // If cumulative received equals ordered, mark order as Received
            if (receivedTotals[orderDetailId] >= detail.Quantity)
            {
                detail.Order.OrderStatus = "Received";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ViewOrders");
        }

        // ==============================
        // BOOK DETAILS (Admin Procurement)
        // ==============================
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.IsInRole("Admin"))
                return View("AccessDenied");

            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Reviews)
                    .ThenInclude(r => r.Reviewer)
                .FirstOrDefaultAsync(b => b.BookID == id);

            if (book == null) return NotFound();

            // Only approved reviews
            book.Reviews = book.Reviews
                .Where(r => r.DisputeStatus == "Approved")
                .ToList();

            // Compute last supplier cost and avg margin for this book
            var lastCost = _context.OrderDetails
                .Where(od => od.BookID == book.BookID &&
                             (od.Order.OrderStatus == "SupplierOrder" || od.Order.OrderStatus == "Received"))
                .OrderByDescending(od => od.Order.OrderDate)
                .Select(od => od.Cost)
                .FirstOrDefault();

            var avgMargin = _context.OrderDetails
                .Where(od => od.BookID == book.BookID && od.Order.OrderStatus == "Ordered")
                .AsEnumerable()
                .Select(od => (od.Price - od.Cost) / od.Price)
                .DefaultIfEmpty(0)
                .Average();

            ViewBag.LastCosts = new Dictionary<int, decimal> { { book.BookID, lastCost } };
            ViewBag.AvgMargins = new Dictionary<int, decimal> { { book.BookID, avgMargin } };

            return View(book);
        }
    }
}
