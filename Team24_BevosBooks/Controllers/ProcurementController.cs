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
            var historicalCosts = await _context.Reorders
                .Where(r => r.ReorderStatus == "Ordered" || r.ReorderStatus == "Received")
                .GroupBy(r => r.BookID)
                .Select(g => new {
                    BookID = g.Key,
                    LastCost = g.OrderByDescending(x => x.Date)
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
                    lastCosts[book.Key] = historicalCosts[book.Key];
                }
                else
                {
                    lastCosts[book.Key] = book.Value;
                }
            }

            // Calculate profit per unit
            var avgMargins = books.ToDictionary(
                b => b.BookID,
                b =>
                {
                    var cost = lastCosts.ContainsKey(b.BookID) ? lastCosts[b.BookID] : b.Cost;
                    return b.Price > 0 ? (b.Price - cost) : 0m;
                });

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

            foreach (var id in bookIds)
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null) continue;

                if (!int.TryParse(Request.Form[$"qty_{id}"], out var qty)) continue;
                if (!decimal.TryParse(Request.Form[$"cost_{id}"], out var cost)) continue;
                if (qty < 0 || cost <= 0) continue;

                var reorder = new Reorder
                {
                    BookID = book.BookID,
                    Quantity = qty,
                    Cost = cost,
                    Date = DateTime.Now,
                    ReorderStatus = "Ordered"
                };
                _context.Reorders.Add(reorder);
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

            var pendingReorders = await _context.Reorders
                .Where(r => r.ReorderStatus == "Ordered")
                .GroupBy(r => r.BookID)
                .Select(g => new { BookID = g.Key, PendingQty = g.Sum(x => x.Quantity) })
                .ToListAsync();

            var result = books.Where(b =>
                b.InventoryQuantity + (pendingReorders.FirstOrDefault(p => p.BookID == b.BookID)?.PendingQty ?? 0)
                < b.ReorderPoint).ToList();

            // Load all books for fallback cost
            var bookCosts = await _context.Books
                .Select(b => new { b.BookID, b.Cost })
                .ToDictionaryAsync(b => b.BookID, b => b.Cost);

            // Load historical supplier costs
            var historicalCosts = await _context.Reorders
                .Where(r => r.ReorderStatus == "Ordered" || r.ReorderStatus == "Received")
                .GroupBy(r => r.BookID)
                .Select(g => new {
                    BookID = g.Key,
                    LastCost = g.OrderByDescending(x => x.Date)
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
                    lastCosts[book.Key] = historicalCosts[book.Key];
                }
                else
                {
                    lastCosts[book.Key] = book.Value;
                }
            }

            // Calculate profit per unit
            var avgMargins = books.ToDictionary(
                b => b.BookID,
                b =>
                {
                    var cost = lastCosts.ContainsKey(b.BookID) ? lastCosts[b.BookID] : b.Cost;
                    return b.Price > 0 ? (b.Price - cost) : 0m;
                });

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

            foreach (var id in bookIds)
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null) continue;

                if (!int.TryParse(Request.Form[$"qty_{id}"], out var qty)) continue;
                if (!decimal.TryParse(Request.Form[$"cost_{id}"], out var cost)) continue;
                if (qty < 0 || cost <= 0) continue;

                var reorder = new Reorder
                {
                    BookID = book.BookID,
                    Quantity = qty,
                    Cost = cost,
                    Date = DateTime.Now,
                    ReorderStatus = "Ordered"
                };
                _context.Reorders.Add(reorder);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("ViewOrders");
        }

        // ==============================
        // VIEW REORDERS
        // ==============================
        [Authorize]
        public async Task<IActionResult> ViewOrders()
        {
            if (!User.IsInRole("Admin"))
                return View("AccessDenied");

            var reorders = await _context.Reorders
                .Include(r => r.Book)
                .OrderByDescending(r => r.Date)
                .ToListAsync();

            // Load dictionary from TempData (deserialize JSON string)
            var receivedTotals = TempData["ReceivedTotals"] != null
                ? JsonSerializer.Deserialize<Dictionary<int, int>>(TempData["ReceivedTotals"].ToString())
                : new Dictionary<int, int>();

            // Compute outstanding balances and remove fulfilled rows
            var outstandingBalances = new Dictionary<int, int>();
            var toRemove = new List<Reorder>();

            foreach (var reorder in reorders)
            {
                int alreadyReceived = receivedTotals.ContainsKey(reorder.ReorderID)
                    ? receivedTotals[reorder.ReorderID]
                    : 0;

                int outstanding = reorder.Quantity - alreadyReceived;

                if (outstanding <= 0)
                {
                    reorder.ReorderStatus = "Received";
                    toRemove.Add(reorder); // mark for deletion
                }
                else
                {
                    outstandingBalances[reorder.ReorderID] = outstanding;
                }
            }

            // Delete fulfilled reorders
            if (toRemove.Any())
            {
                _context.Reorders.RemoveRange(toRemove);
                await _context.SaveChangesAsync();
                reorders = reorders.Except(toRemove).ToList();
            }

            // Keep dictionary available for next request
            TempData["ReceivedTotals"] = JsonSerializer.Serialize(receivedTotals);
            TempData.Keep("ReceivedTotals");

            ViewBag.ReceivedTotals = receivedTotals;
            ViewBag.OutstandingBalances = outstandingBalances;

            return View(reorders);
        }

        // ==============================
        // CHECK IN ARRIVALS
        // ==============================
        [HttpPost]
        public async Task<IActionResult> CheckIn(int reorderId, int arrivedQty)
        {
            var reorder = await _context.Reorders
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.ReorderID == reorderId);

            if (reorder == null) return NotFound();

            if (arrivedQty < 0) arrivedQty = 0;

            // Load dictionary from TempData
            var receivedTotals = TempData["ReceivedTotals"] != null
                ? JsonSerializer.Deserialize<Dictionary<int, int>>(TempData["ReceivedTotals"].ToString())
                : new Dictionary<int, int>();

            int alreadyReceived = receivedTotals.ContainsKey(reorderId)
                ? receivedTotals[reorderId]
                : 0;

            // Clamp so we never exceed ordered quantity
            if (alreadyReceived + arrivedQty > reorder.Quantity)
            {
                arrivedQty = reorder.Quantity - alreadyReceived;
            }

            // Update dictionary
            receivedTotals[reorderId] = alreadyReceived + arrivedQty;

            // Save dictionary back to TempData
            TempData["ReceivedTotals"] = JsonSerializer.Serialize(receivedTotals);

            // Update inventory
            reorder.Book.InventoryQuantity += arrivedQty;

            // If cumulative received equals ordered, mark reorder as Received and delete
            if (receivedTotals[reorderId] >= reorder.Quantity)
            {
                reorder.ReorderStatus = "Received";
                _context.Reorders.Remove(reorder);
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
            var lastCost = _context.Reorders
                .Where(r => r.BookID == book.BookID &&
                            (r.ReorderStatus == "Ordered" || r.ReorderStatus == "Received"))
                .OrderByDescending(r => r.Date)
                .Select(r => r.Cost)
                .FirstOrDefault();

            var avgMargin = _context.Reorders
                .Where(r => r.BookID == book.BookID && r.ReorderStatus == "Ordered")
                .AsEnumerable()
                .Select(r => (book.Price - r.Cost) / book.Price)
                .DefaultIfEmpty(0)
                .Average();

            ViewBag.LastCosts = new Dictionary<int, decimal> { { book.BookID, lastCost } };
            ViewBag.AvgMargins = new Dictionary<int, decimal> { { book.BookID, avgMargin } };

            return View(book);
        }
    }
}
