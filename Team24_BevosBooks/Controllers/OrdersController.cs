using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private const decimal FIRST_BOOK_SHIPPING = 3.50m;
        private const decimal ADDITIONAL_BOOK_SHIPPING = 1.50m;

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
        // CART TOTALS
        // ============================================================
        private (decimal subtotal, decimal shipping, decimal total)
            ComputeCartTotals(Order cart, bool freeShipping = false)
        {
            decimal subtotal = cart.OrderDetails.Sum(od => od.Price * od.Quantity);
            int qty = cart.OrderDetails.Sum(od => od.Quantity);

            decimal shipping = 0m;
            if (!freeShipping && qty > 0)
            {
                shipping = FIRST_BOOK_SHIPPING + (qty - 1) * ADDITIONAL_BOOK_SHIPPING;
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
            var totals = ComputeCartTotals(cart);

            ViewBag.Messages = messages;
            ViewBag.Subtotal = totals.subtotal;
            ViewBag.Shipping = totals.shipping;
            ViewBag.Total = totals.total;

            return View(cart);
        }

        // ============================================================
        // ADD TO CART
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

            var existing = await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.OrderID == cart.OrderID &&
                                           od.BookID == id);

            if (existing == null)
            {
                _context.OrderDetails.Add(new OrderDetail
                {
                    OrderID = cart.OrderID,
                    BookID = id,
                    Quantity = 1,
                    Price = book.Price,
                    Cost = book.Cost
                });
            }
            else
            {
                if (existing.Quantity < book.InventoryQuantity)
                    existing.Quantity++;
                else
                    TempData["CartMessage"] = "Cannot exceed stock quantity.";
            }

            await _context.SaveChangesAsync();
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
        // CHECKOUT (GET)
        // ============================================================
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);

            var cart = await GetOrCreateCart(user.Id);

            cart = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                .FirstAsync(o => o.OrderID == cart.OrderID);

            if (!cart.OrderDetails.Any())
            {
                TempData["CheckoutMessage"] = "Your cart is empty.";
                return RedirectToAction("Cart");
            }

            await UpdateCartForChanges(cart);

            var cards = await _context.Cards
                .Where(c => c.UserID == user.Id)
                .ToListAsync();

            var totals = ComputeCartTotals(cart);

            var vm = new CheckoutViewModel
            {
                Order = cart,
                Cards = cards,
                SelectedCardID = cards.Any() ? cards.First().CardID : null,
                CouponCode = "",
                Subtotal = totals.subtotal,
                Shipping = totals.shipping,
                Total = totals.total
            };

            return View(vm);
        }

        // ============================================================
        // CHECKOUT (POST)
        // ============================================================
        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            ModelState.Remove(nameof(CheckoutViewModel.CouponCode));

            var cart = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                .FirstOrDefaultAsync(o => o.UserID == user.Id &&
                                          o.OrderStatus == "InCart");

            if (cart == null)
                return NotFound();

            if (!cart.OrderDetails.Any())
            {
                TempData["CheckoutMessage"] = "Your cart is empty.";
                return RedirectToAction("Cart");
            }

            await UpdateCartForChanges(cart);

            var cards = await _context.Cards.Where(c => c.UserID == user.Id).ToListAsync();

            model.Order = cart;
            model.Cards = cards;

            if (!cards.Any())
                ModelState.AddModelError("", "You must add a credit card before checking out.");

            if (model.SelectedCardID == null)
                ModelState.AddModelError(nameof(model.SelectedCardID), "Please select a credit card.");

            bool freeShipping = false;
            Coupon? coupon = null;
            bool applied = false;
            string error = null;

            // ---------------- COUPON LOGIC ----------------
            if (!string.IsNullOrWhiteSpace(model.CouponCode))
            {
                coupon = await _context.Coupons
                    .FirstOrDefaultAsync(c => c.CouponCode == model.CouponCode.ToUpper() &&
                                              c.Status == "Enabled");

                if (coupon == null)
                {
                    error = "This coupon is invalid or disabled.";
                }
                else
                {
                    // Enforce one use per customer
                    bool used = await _context.Orders
                        .AnyAsync(o => o.UserID == user.Id &&
                                       o.OrderStatus == "Completed" &&
                                       o.CouponID == coupon.CouponID);

                    if (used)
                    {
                        error = "You have already used this coupon before.";
                    }
                    else
                    {
                        applied = true;
                    }
                }
            }

            // Reset all prices before applying discounts
            foreach (var od in cart.OrderDetails)
            {
                od.Price = od.Book.Price;
                od.CouponID = null;
            }

            if (applied && coupon != null)
            {
                if (coupon.CouponType == "PercentOff")
                {
                    if (!coupon.DiscountPercent.HasValue || coupon.DiscountPercent.Value <= 0)
                    {
                        applied = false;
                        error = "This coupon requires a valid percent off value.";
                    }
                    else
                    {
                        foreach (var od in cart.OrderDetails)
                        {
                            decimal discount = od.Price * coupon.DiscountPercent.Value / 100m;
                            od.Price -= discount;
                            od.CouponID = coupon.CouponID;
                        }
                    }
                }
                else if (coupon.CouponType == "FreeShipping")
                {
                    var subtotalBefore = cart.OrderDetails.Sum(od => od.Price * od.Quantity);

                    if (!coupon.FreeThreshold.HasValue)
                    {
                        // ✅ Free shipping for all orders
                        freeShipping = true;
                        foreach (var od in cart.OrderDetails)
                            od.CouponID = coupon.CouponID;
                    }
                    else if (subtotalBefore >= coupon.FreeThreshold.Value)
                    {
                        // ✅ Free shipping above threshold
                        freeShipping = true;
                        foreach (var od in cart.OrderDetails)
                            od.CouponID = coupon.CouponID;
                    }
                    else
                    {
                        applied = false;
                        error = $"This coupon requires a total of {coupon.FreeThreshold:c}.";
                    }
                }
            }

            // ---------------- SEND EMAIL ----------------
            string listHtml = string.Join("", cart.OrderDetails
                .Select(od => $"<li>{od.Book.Title} — {od.Quantity} × {od.Price:C}</li>"));

            string emailHtml = EmailTemplate.Wrap($@"
                <h2>Your Order Is Confirmed!</h2>
                <p>Hello {user.FirstName},</p>
                <p>Your order <strong>#{cart.OrderID}</strong> has been placed.</p>
                <ul>{listHtml}</ul>
                <p><strong>Subtotal:</strong> {model.Subtotal:C}<br/>
                   <strong>Shipping:</strong> {model.Shipping:C}<br/>
                   <strong>Total:</strong> {model.Total:C}</p>
            ");

            await _emailSender.SendEmailAsync(
                user.Email,
                "Bevo’s Books — Order Confirmation",
                emailHtml
            );

            return RedirectToAction("OrderConfirmation", new { id = cart.OrderID });
        }

        // ============================================================
        // ORDER CONFIRMATION
        // ============================================================
        [Authorize]
        public async Task<IActionResult> OrderConfirmation(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                        .ThenInclude(b => b.Genre)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Card)
                .FirstOrDefaultAsync(o => o.OrderID == id &&
                                          o.UserID == user.Id);

            if (order == null)
                return NotFound();

            // BETTER recommendation logic
            var firstBook = order.OrderDetails.FirstOrDefault()?.Book;

            var recs = firstBook == null
                ? new List<Book>()
                : await _context.Books
                    .Where(b =>
                        b.GenreID == firstBook.GenreID &&
                        b.BookStatus == "Active" &&
                        !order.OrderDetails.Select(od => od.BookID).Contains(b.BookID))
                    .Take(3)
                    .ToListAsync();

            ViewBag.Recommendations = recs;

            return View(order);
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
                    .ThenInclude(od => od.Book)   // <-- load Book
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Card)   // <-- load Card
                .Where(o => o.UserID == user.Id &&
                            o.OrderStatus == "Completed")
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // ============================================================
        // ALL ORDERS
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
    }
}