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
        // MANUAL REORDER
        // ==============================
        public async Task<IActionResult> ManualReorder()
        {
            var books = await _context.Books
                .Where(b => b.BookStatus == "Active")
                .OrderBy(b => b.Title)
                .ToListAsync();

            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> ManualReorder(int bookId, int quantity, decimal? cost)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return NotFound();

            // Default to last cost if not provided
            decimal reorderCost = cost ?? book.Cost;

            // Create supplier order
            var order = new Order
            {
                UserID = null, // no customer
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
                Price = book.Price, // selling price stays same
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
            var books = await _context.Books
                .Where(b => b.InventoryQuantity < b.ReorderPoint && b.BookStatus == "Active")
                .ToListAsync();

            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> AutoReorder(List<int> bookIds)
        {
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
                .Where(o => o.OrderStatus == "SupplierOrder")
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
                .FirstOrDefaultAsync(od => od.OrderDetailID == orderDetailId);

            if (detail == null) return NotFound();

            // Only allow up to ordered quantity
            int qtyToAdd = Math.Min(arrivedQty, detail.Quantity);

            detail.Book.InventoryQuantity += qtyToAdd;

            // If fully received, mark order as completed
            if (qtyToAdd == detail.Quantity)
            {
                detail.Order.OrderStatus = "Received";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ViewOrders");
        }
    }
}
