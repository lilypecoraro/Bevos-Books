using Microsoft.AspNetCore.Authorization;
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

        public ProcurementController(AppDbContext context)
        {
            _context = context;
        }

        // ==============================
        // MANUAL REORDER (with search)
        // ==============================
        public async Task<IActionResult> ManualReorder(string searchString)
        {
            var query = _context.Books.Where(b => b.BookStatus == "Active");

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(b => b.Title.Contains(searchString) ||
                                         b.Authors.Contains(searchString));
            }

            var books = await query.OrderBy(b => b.Title).ToListAsync();
            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> ManualReorder(int bookId, int quantity, decimal? cost)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return NotFound();

            if (quantity <= 0) ModelState.AddModelError("", "Quantity must be greater than zero.");
            if (cost <= 0) ModelState.AddModelError("", "Cost must be greater than zero.");
            if (!ModelState.IsValid) return RedirectToAction("ManualReorder");

            decimal reorderCost = cost ?? book.Cost;

            var order = new Order
            {
                UserID = null, // supplier order, not tied to customer
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
                Quantity = quantity,
                Price = book.Price,
                Cost = reorderCost
            };

            _context.OrderDetails.Add(detail);
            await _context.SaveChangesAsync();

            return RedirectToAction("ViewOrders");
        }

        // ==============================
        // AUTO REORDER
        // ==============================
        public async Task<IActionResult> AutoReorder()
        {
            // Books below reorder point
            var books = await _context.Books
                .Where(b => b.BookStatus == "Active")
                .ToListAsync();

            // Exclude books already covered by pending supplier orders
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
        public async Task<IActionResult> AutoReorder(List<int> bookIds)
        {
            if (bookIds == null || bookIds.Count == 0)
                return RedirectToAction("AutoReorder");

            foreach (var id in bookIds)
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null) continue;

                var order = new Order
                {
                    UserID = null,
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
                    Quantity = 5, // default reorder qty
                    Price = book.Price,
                    Cost = book.Cost
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

            if (arrivedQty < 0) ModelState.AddModelError("", "Arrived quantity cannot be negative.");
            if (arrivedQty > detail.Quantity) ModelState.AddModelError("", "Arrived quantity cannot exceed ordered quantity.");
            if (!ModelState.IsValid) return RedirectToAction("ViewOrders");

            detail.Book.InventoryQuantity += arrivedQty;

            // If fully received, mark order as completed
            if (arrivedQty == detail.Quantity)
            {
                detail.Order.OrderStatus = "Received";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ViewOrders");
        }
    }
}
