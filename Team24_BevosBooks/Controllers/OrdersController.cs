using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using Team24_BevosBooks.Models.ViewModels;

namespace Team24_BevosBooks.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        private const decimal FIRST_BOOK_SHIPPING = 3.50m;
        private const decimal ADDITIONAL_BOOK_SHIPPING = 1.50m;

        public OrdersController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ==============================================
        // GET OR CREATE CART
        // ==============================================
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

        // ==============================================
        // UPDATE CART (discontinued / stock / price)
        // ==============================================
        private async Task<List<string>> UpdateCartForChanges(Order cart)
        {
            List<string> messages = new();

            var items = cart.OrderDetails.ToList();
            foreach (var item in items)
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

        // ==============================================
        // CART TOTALS
        // ==============================================
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

        // ==============================================
        // CART PAGE
        // ==============================================
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Cart()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

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

        // ==============================================
        // ADD TO CART
        // ==============================================
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

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
                existing = new OrderDetail
                {
                    OrderID = cart.OrderID,
                    BookID = id,
                    Quantity = 1,
                    Price = book.Price,
                    Cost = book.Cost
                };

                _context.OrderDetails.Add(existing);
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

        // ==============================================
        // REMOVE ITEM
        // ==============================================
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

        // ==============================================
        // UPDATE QUANTITY
        // ==============================================
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int orderDetailId, int quantity)
        {
            var detail = await _context.OrderDetails
                .Include(od => od.Book)
                .Include(od => od.Order)
                .FirstOrDefaultAsync(od => od.OrderDetailID == orderDetailId);

            if (detail == null) return NotFound();

            if (quantity <= 0)
                _context.OrderDetails.Remove(detail);
            else
                detail.Quantity = Math.Min(quantity, detail.Book.InventoryQuantity);

            await _context.SaveChangesAsync();
            return RedirectToAction("Cart");
        }

        // ==============================================
        // CHECKOUT (GET)
        // ==============================================
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

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
                SelectedCardID = cards.Any() ? cards.First().CardID : (int?)null,
                CouponCode = "",
                Subtotal = totals.subtotal,
                Shipping = totals.shipping,
                Total = totals.total
            };

            if (TempData["CheckoutMessage"] != null)
            {
                ViewBag.CheckoutMessage = TempData["CheckoutMessage"];
            }

            return View(vm);
        }

        // ==============================================
        // CHECKOUT (POST)
        // ==============================================
        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            // Coupon is optional – ignore any implicit "required" validation on it
            ModelState.Remove(nameof(CheckoutViewModel.CouponCode));

            // Safety: Order might not be bound – get cart by user instead
            var cart = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                .FirstOrDefaultAsync(o => o.UserID == user.Id &&
                                          o.OrderStatus == "InCart");

            if (cart == null) return NotFound();

            if (!cart.OrderDetails.Any())
            {
                TempData["CheckoutMessage"] = "Your cart is empty.";
                return RedirectToAction("Cart");
            }

            await UpdateCartForChanges(cart);

            var cards = await _context.Cards
                .Where(c => c.UserID == user.Id)
                .ToListAsync();

            model.Order = cart;
            model.Cards = cards;

            if (!cards.Any())
            {
                ModelState.AddModelError(string.Empty, "You must add a credit card before checking out.");
            }

            if (model.SelectedCardID == null)
            {
                ModelState.AddModelError(nameof(model.SelectedCardID), "Please select a credit card.");
            }

            // ---------------- COUPON VALIDATION/APPLICATION ----------------
            Coupon? coupon = null;
            bool freeShipping = false;
            bool couponApplied = false;
            string? couponMessage = null;

            if (!string.IsNullOrWhiteSpace(model.CouponCode))
            {
                coupon = await _context.Coupons
                    .FirstOrDefaultAsync(c => c.CouponCode == model.CouponCode &&
                                              c.Status == "Enabled");

                if (coupon == null)
                {
                    couponMessage = "This coupon code is invalid or disabled.";
                }
                else
                {
                    bool alreadyUsed = await _context.OrderDetails
                        .Include(od => od.Order)
                        .AnyAsync(od => od.Order.UserID == user.Id &&
                                        od.Order.OrderStatus == "Completed" &&
                                        od.CouponID == coupon.CouponID);

                    if (alreadyUsed)
                    {
                        couponMessage = "You have already used this coupon.";
                    }
                    else
                    {
                        couponApplied = true;
                    }
                }
            }

            foreach (var od in cart.OrderDetails)
            {
                od.Price = od.Book.Price;
                od.CouponID = null;
            }

            if (couponApplied && coupon != null)
            {
                if (coupon.CouponType == "PercentOff")
                {
                    foreach (var od in cart.OrderDetails)
                    {
                        decimal discount = od.Price * (coupon.DiscountPercent.Value / 100m);
                        od.Price -= discount;
                        od.CouponID = coupon.CouponID;
                    }
                }
                else if (coupon.CouponType == "FreeShipping")
                {
                    decimal before = cart.OrderDetails.Sum(od => od.Price * od.Quantity);
                    if (before >= coupon.FreeThreshold)
                    {
                        freeShipping = true;
                        foreach (var od in cart.OrderDetails)
                        {
                            od.CouponID = coupon.CouponID;
                        }
                    }
                    else
                    {
                        couponMessage = $"This coupon requires an order total of at least {coupon.FreeThreshold:c}.";
                        couponApplied = false;
                    }
                }
            }

            var totals = ComputeCartTotals(cart, freeShipping);
            model.Subtotal = cart.OrderDetails.Sum(od => od.Price * od.Quantity);
            model.Shipping = totals.shipping;
            model.Total = totals.total;

            if (!string.IsNullOrEmpty(couponMessage))
            {
                ViewBag.CheckoutMessage = couponMessage;
            }

            if (!ModelState.IsValid || !cards.Any())
            {
                return View(model);
            }

            var card = await _context.Cards
                .FirstOrDefaultAsync(c => c.CardID == model.SelectedCardID &&
                                          c.UserID == user.Id);

            if (card == null)
            {
                ModelState.AddModelError(nameof(model.SelectedCardID), "Selected card is invalid.");
                return View(model);
            }

            foreach (var item in cart.OrderDetails)
            {
                var book = await _context.Books.FindAsync(item.BookID);
                if (book != null)
                    book.InventoryQuantity -= item.Quantity;
            }

            cart.OrderStatus = "Completed";
            cart.OrderDate = DateTime.Now;
            cart.ShippingFee = totals.shipping;

            foreach (var od in cart.OrderDetails)
            {
                od.CardID = card.CardID;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("OrderConfirmation", new { id = cart.OrderID });
        }

        // ==============================================
        // ORDER CONFIRMATION + RECOMMENDATIONS
        // ==============================================
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

            var firstDetailWithBook = order.OrderDetails.FirstOrDefault(od => od.Book != null);

            List<Book> recs = new List<Book>();

            if (firstDetailWithBook != null)
            {
                var firstBook = firstDetailWithBook.Book;

                recs = await _context.Books
                    .Where(b => b.GenreID == firstBook.GenreID &&
                                b.BookStatus == "Active" &&
                                !order.OrderDetails.Select(od => od.BookID).Contains(b.BookID))
                    .Take(3)
                    .ToListAsync();
            }

            ViewBag.Recommendations = recs;

            return View(order);
        }

        // ==============================================
        // ORDER HISTORY
        // ==============================================
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> History()
        {
            var user = await _userManager.GetUserAsync(User);

            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Card)
                .Where(o => o.UserID == user.Id &&
                            o.OrderStatus == "Completed")
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // ==============================================
        // ALL ORDERS (ADMIN/EMPLOYEE)
        // ==============================================
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