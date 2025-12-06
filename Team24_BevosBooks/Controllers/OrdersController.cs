using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using Team24_BevosBooks.Models.ViewModels;
using Team24_BevosBooks.Services;

namespace Team24_BevosBooks.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;

        public OrdersController(AppDbContext context,
                                UserManager<AppUser> userManager,
                                IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // ============================================================
        // GET OR CREATE CART
        // ============================================================
        private async Task<Order> GetOrCreateCart(string userId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                .FirstOrDefaultAsync(o => o.UserID == userId &&
                                          o.OrderStatus == "InCart");

            if (order == null)
            {
                order = new Order
                {
                    UserID = userId,
                    OrderDate = DateTime.Now,
                    OrderStatus = "InCart",
                    ShippingFee = 0,
                    OrderDetails = new List<OrderDetail>()
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }

            return order;
        }

        // ============================================================
        // UPDATE CART (discontinued / stock / price)
        // ============================================================
        private async Task<List<string>> UpdateCartForChanges(Order cart)
        {
            List<string> messages = new();

            foreach (var item in cart.OrderDetails.ToList())
            {
                var book = await _context.Books.FindAsync(item.BookID);

                if (book == null || book.BookStatus == "Discontinued")
                {
                    messages.Add($"'{item.Book?.Title}' was discontinued and removed from your cart.");
                    _context.OrderDetails.Remove(item);
                    continue;
                }

                if (book.InventoryQuantity == 0)
                {
                    messages.Add($"'{book.Title}' is out of stock and was removed from your cart.");
                    _context.OrderDetails.Remove(item);
                    continue;
                }

                if (item.Quantity > book.InventoryQuantity)
                {
                    item.Quantity = book.InventoryQuantity;
                    messages.Add($"Quantity for '{book.Title}' reduced to {book.InventoryQuantity} (max in stock).");
                }

                item.Price = book.Price;
                item.Cost = book.Cost;
            }

            await _context.SaveChangesAsync();
            return messages;
        }

        // ============================================================
        // CART TOTALS (uses ShippingSetting table)
        // ============================================================
        private async Task<(decimal subtotal, decimal shipping, decimal total)>
            ComputeCartTotals(Order cart, bool freeShipping = false)
        {
            var settings = await _context.ShippingSettings.FirstAsync();

            decimal subtotal = cart.OrderDetails.Sum(od => od.Price * od.Quantity);
            int qty = cart.OrderDetails.Sum(od => od.Quantity);

            decimal shipping = 0m;
            if (!freeShipping && qty > 0)
            {
                shipping = settings.FirstBookRate + (qty - 1) * settings.AdditionalBookRate;
            }

            return (subtotal, shipping, subtotal + shipping);
        }

        // ============================================================
        // CART PAGE
        // ============================================================
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Cart()
        {
            var user = await _userManager.GetUserAsync(User);

            var cart = await GetOrCreateCart(user.Id);

            cart = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                .FirstAsync(o => o.OrderID == cart.OrderID);

            var messages = await UpdateCartForChanges(cart);
            var totals = await ComputeCartTotals(cart);

            ViewBag.Messages = messages;
            ViewBag.Subtotal = totals.subtotal;
            ViewBag.Shipping = totals.shipping;
            ViewBag.Total = totals.total;

            return View(cart);
        }

        // ============================================================
        // ADD TO CART (fixed)
        // ============================================================
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            if (book.InventoryQuantity <= 0 || book.BookStatus == "Discontinued")
            {
                TempData["CartMessage"] = "This book is not available.";
                return RedirectToAction("Details", "Books", new { id });
            }

            var cart = await GetOrCreateCart(user.Id);

            // Look for existing item in this cart
            var existing = await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.OrderID == cart.OrderID && od.BookID == id);

            if (existing == null)
            {
                var newDetail = new OrderDetail
                {
                    OrderID = cart.OrderID,
                    Order = cart,              // ✅ link navigation property
                    BookID = id,
                    Book = book,               // ✅ link navigation property
                    Quantity = 1,
                    Price = book.Price,
                    Cost = book.Cost
                };

                _context.OrderDetails.Add(newDetail);
            }
            else
            {
                if (existing.Quantity < book.InventoryQuantity)
                {
                    existing.Quantity++;
                }
                else
                {
                    TempData["CartMessage"] = "Cannot exceed stock quantity.";
                }
            }

            await _context.SaveChangesAsync();

            // Debug logging (optional)
            Console.WriteLine($"Cart {cart.OrderID} now has {_context.OrderDetails.Count(od => od.OrderID == cart.OrderID)} items.");

            return RedirectToAction("Cart");
        }

        // ============================================================
        // REMOVE ITEM
        // ============================================================
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> RemoveItem(int orderDetailId)
        {
            var detail = await _context.OrderDetails.FindAsync(orderDetailId);

            if (detail != null)
            {
                _context.OrderDetails.Remove(detail);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Cart");
        }

        // ============================================================
        // UPDATE QUANTITY
        // ============================================================
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int orderDetailId, int quantity)
        {
            var detail = await _context.OrderDetails
                .Include(od => od.Book)
                .FirstOrDefaultAsync(od => od.OrderDetailID == orderDetailId);

            if (detail == null)
                return NotFound();

            if (quantity <= 0)
            {
                _context.OrderDetails.Remove(detail);
            }
            else if (quantity > detail.Book.InventoryQuantity)
            {
                detail.Quantity = detail.Book.InventoryQuantity;
                TempData["CartMessage"] =
                    $"Cannot exceed stock quantity for '{detail.Book.Title}'. Available: {detail.Book.InventoryQuantity}.";
            }
            else
            {
                detail.Quantity = quantity;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Cart");
        }

        // ============================================================
        // ORDER HISTORY
        // ============================================================
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> History()
        {
            var user = await _userManager.GetUserAsync(User);

            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Card)
                .Where(o => o.UserID == user.Id &&
                            o.OrderStatus == "Completed")
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // ============================================================
        // ALL ORDERS (Admin/Employee)
        // ============================================================
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> AllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // ============================================================
        // CHECKOUT PAGE (GET)
        // ============================================================
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);

            // Get or create the cart for this user
            var cart = await GetOrCreateCart(user.Id);

            // Reload the cart with OrderDetails and Books included
            cart = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                .FirstOrDefaultAsync(o => o.OrderID == cart.OrderID);

            // Ensure OrderDetails is not null
            cart.OrderDetails ??= new List<OrderDetail>();

            // Update cart for discontinued/out-of-stock changes
            var messages = await UpdateCartForChanges(cart);

            // Compute totals
            var totals = await ComputeCartTotals(cart);

            // Get saved cards for this user
            var cards = await _context.Cards
                .Where(c => c.User.Id == user.Id)
                .ToListAsync();

            // Build the view model
            var vm = new CheckoutViewModel
            {
                Order = cart,
                Cards = cards,
                Subtotal = totals.subtotal,
                Shipping = totals.shipping,
                Total = totals.total
            };

            ViewBag.Messages = messages;
            return View(vm); // ✅ Pass CheckoutViewModel to the view
        }


        // ============================================================
        // CHECKOUT (POST) - Place Order
        // ============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid || model.Order == null)
            {
                return View(model);
            }

            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderID == model.Order.OrderID);

            if (order == null) return NotFound();

            // Apply coupon logic if needed
            if (!string.IsNullOrEmpty(model.CouponCode))
            {
                ViewBag.CheckoutMessage = $"Coupon {model.CouponCode} applied!";
                // TODO: adjust totals here if coupon discounts apply
            }

            // Mark order as completed
            order.OrderStatus = "Completed";
            order.OrderDate = DateTime.Now;

            await _context.SaveChangesAsync();

            // Optionally send confirmation email
            await _emailSender.SendEmailAsync(
                User.Identity.Name,
                "Order Confirmation",
                $"Your order #{order.OrderID} has been placed successfully."
            );

            return RedirectToAction("History");
        }

    }
}
