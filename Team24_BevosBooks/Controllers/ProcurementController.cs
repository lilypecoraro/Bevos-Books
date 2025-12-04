using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> ManualReorder(string searchString, string genre, string sortOrder, bool inStockOnly = false)
        {
            var query = _context.Books
                .Include(b => b.Genre)
                .Where(b => b.BookStatus == "Active");

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(b => b.Title.Contains(searchString) ||
                                         b.Authors.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(b => b.Genre.GenreName == genre);
            }

            if (inStockOnly)
            {
                query = query.Where(b => b.InventoryQuantity > 0);
            }

            // Sorting (only Title/Author for now)
            query = sortOrder switch
            {
                "author" => query.OrderBy(b => b.Authors),
                _ => query.OrderBy(b => b.Title)
            };

            ViewBag.Genres = await _context.Genres.Select(g => g.GenreName).ToListAsync();
            var books = await query.ToListAsync();
            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> ManualReorder(List<int> bookIds)
        {
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
        public async Task<IActionResult> AutoReorder()
        {
            var books = await _context.Books
                .Where(b => b.BookStatus == "Active")
                .ToListAsync();

            var pendingOrders = await _context.OrderDetails
                .Include(od => od.Order)
                .Where(od => od.Order.OrderStatus == "SupplierOrder")
                .GroupBy(od => od.BookID)
                .Select(g => new { BookID = g.Key, PendingQty = g.Sum(x => x.Quantity) })
                .ToListAsync();

            var result = books.Where(b =>
                b.InventoryQuantity + (pendingOrders.FirstOrDefault(p => p.BookID == b.BookID)?.PendingQty ?? 0)
                < b.ReorderPoint).ToList();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> AutoReorder(List<int> bookIds, List<int> removeIds)
        {
            if (bookIds == null || bookIds.Count == 0)
                return RedirectToAction("AutoReorder");

            var adminId = _userManager.GetUserId(User);

            foreach (var id in bookIds)
            {
                if (removeIds != null && removeIds.Contains(id)) continue;

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
        // VIEW SUPPLIER ORDERS
        // ==============================
        public async Task<IActionResult> ViewOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                .Where(o => o.OrderStatus == "SupplierOrder" || o.OrderStatus == "Received")
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

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

            if (arrivedQty > detail.Quantity) arrivedQty = detail.Quantity;
            if (arrivedQty < 0) arrivedQty = 0;

            detail.Book.InventoryQuantity += arrivedQty;

            if (arrivedQty == detail.Quantity)
            {
                detail.Order.OrderStatus = "Received";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ViewOrders");
        }
    }
}
