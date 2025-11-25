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

namespace Team24_BevosBooks.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        // Shipping constants (can be changed later without editing logic)
        private const decimal FIRST_BOOK_SHIPPING = 3.50m;
        private const decimal ADDITIONAL_BOOK_SHIPPING = 1.50m;

        public OrdersController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ======================================
        // Helpers
        // ======================================

        // Get or create current user's "InCart" order
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
                    ShippingFee = 0m,
                    OrderStatus = "InCart",
                    OrderDetails = new List<OrderDetail>()
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }

            return order;
        }

        // Make sure cart reflects current world:
        // - remove discontinued, out-of-stock
        // - adjust quantities down to inventory
        // - update prices and costs to current Book values
        private async Task<List<string>> UpdateCartForChanges(Order cart)
        {
            var messages = new List<string>();

            var details = cart.OrderDetails.ToList();
            foreach (var detail in details)
            {
                var book = await _context.Books.FirstOrDefaultAsync(b => b.BookID == detail.BookID);
                if (book == null)
                {
                    messages.Add($"'{detail.Book?.Title}' is no longer available and was removed from your cart.");
                    _context.OrderDetails.Remove(detail);
                    continue;
                }

                if (book.BookStatus == "Discontinued")
                {
                    messages.Add($"'{book.Title}' has been discontinued and was removed from your cart.");
                    _context.OrderDetails.Remove(detail);
                    continue;
                }

                if (book.InventoryQuantity == 0)
                {
                    messages.Add($"'{book.Title}' is now out of stock and was removed from your cart.");
                    _context.OrderDetails.Remove(detail);
                    continue;
                }

                if (detail.Quantity > book.InventoryQuantity)
                {
                    detail.Quantity = book.InventoryQuantity;
                    messages.Add($"Quantity for '{book.Title}' was reduced to {book.InventoryQuantity} due to stock limits.");
                }

                // Always reflect current price/cost
                detail.Price = book.Price;
                detail.Cost = book.Cost;
            }

            await _context.SaveChangesAsync();
            return messages;
        }

        // Totals for an IN-CART order (before it's placed)
        private (decimal subtotal, decimal shipping, decimal total)
            ComputeCartTotals(Order cart, bool freeShipping = false)
        {
            decimal subtotal = cart.OrderDetails.Sum(od => od.Quantity * od.Price);
            int totalQty = cart.OrderDetails.Sum(od => od.Quantity);

            decimal shipping = 0m;
            if (!freeShipping && totalQty > 0)
            {
                shipping = FIRST_BOOK_SHIPPING +
                           (totalQty - 1) * ADDITIONAL_BOOK_SHIPPING;
            }

            cart.ShippingFee = shipping;
            decimal total = subtotal + shipping;
            return (subtotal, shipping, total);
        }

        // Totals for a COMPLETED order (use stored shipping fee)
        private (decimal subtotal, decimal shipping, decimal total)
            ComputeStoredOrderTotals(Order order)
        {
            decimal subtotal = order.OrderDetails.Sum(od => od.Quantity * od.Price);
            decimal shipping = order.ShippingFee;
            decimal total = subtotal + shipping;
            return (subtotal, shipping, total);
        }

        // ======================================
        // CART
        // ======================================

        // GET: /Orders/Cart
        public async Task<IActionResult> Cart()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var cart = await GetOrCreateCart(user.Id);

            // Reload with navigation props
            cart = await _context.Orders
                                 .Include(o => o.OrderDetails)
                                 .ThenInclude(od => od.Book)
                                 .FirstAsync(o => o.OrderID == cart.OrderID);

            var messages = await UpdateCartForChanges(cart);
            var totals = ComputeCartTotals(cart);

            await _context.SaveChangesAsync();

            ViewBag.Messages = messages;
            ViewBag.Subtotal = totals.subtotal;
            ViewBag.Shipping = totals.shipping;
            ViewBag.Total = totals.total;

            return View(cart);
        }

        // GET: /Orders/AddToCart/5 (called from Book Details)
        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookID == id);
            if (book == null) return NotFound();

            if (book.InventoryQuantity <= 0 || book.BookStatus == "Discontinued")
            {
                TempData["CartMessage"] = "This book is not available to add to the cart.";
                return RedirectToAction("Details", "Books", new { id = book.BookID });
            }

            var cart = await GetOrCreateCart(user.Id);

            var existingDetail = await _context.OrderDetails
                                               .Include(od => od.Book)
                                               .FirstOrDefaultAsync(od => od.OrderID == cart.OrderID &&
                                                                          od.BookID == book.BookID);

            if (existingDetail == null)
            {
                existingDetail = new OrderDetail
                {
                    OrderID = cart.OrderID,
                    BookID = book.BookID,
                    Quantity = 1,
                    Price = book.Price,
                    Cost = book.Cost
                };
                _context.OrderDetails.Add(existingDetail);
            }
            else
            {
                if (existingDetail.Quantity < book.InventoryQuantity)
                {
                    existingDetail.Quantity += 1;
                }
                else
                {
                    TempData["CartMessage"] = "You cannot add more of this book than are in stock.";
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Cart");
        }

        // POST: /Orders/UpdateQuantity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity(int orderDetailId, int quantity)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var detail = await _context.OrderDetails
                                       .Include(od => od.Order)
                                       .Include(od => od.Book)
                                       .FirstOrDefaultAsync(od => od.OrderDetailID == orderDetailId &&
                                                                  od.Order.UserID == user.Id &&
                                                                  od.Order.OrderStatus == "InCart");

            if (detail == null) return NotFound();

            if (quantity <= 0)
            {
                _context.OrderDetails.Remove(detail);
            }
            else
            {
                var book = detail.Book;
                if (book.InventoryQuantity == 0)
                {
                    _context.OrderDetails.Remove(detail);
                }
                else
                {
                    detail.Quantity = Math.Min(quantity, book.InventoryQuantity);
                    detail.Price = book.Price;
                    detail.Cost = book.Cost;
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Cart");
        }

        // POST: /Orders/RemoveItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveItem(int orderDetailId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var detail = await _context.OrderDetails
                                       .Include(od => od.Order)
                                       .FirstOrDefaultAsync(od => od.OrderDetailID == orderDetailId &&
                                                                  od.Order.UserID == user.Id &&
                                                                  od.Order.OrderStatus == "InCart");

            if (detail != null)
            {
                _context.OrderDetails.Remove(detail);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Cart");
        }

        // ======================================
        // CHECKOUT
        // ======================================

        // GET: /Orders/Checkout
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
                TempData["CartMessage"] = "You cannot check out with an empty cart.";
                return RedirectToAction("Cart");
            }

            var totals = ComputeCartTotals(cart);
            await _context.SaveChangesAsync();

            ViewBag.Subtotal = totals.subtotal;
            ViewBag.Shipping = totals.shipping;
            ViewBag.Total = totals.total;

            var cards = await _context.Cards
                                      .Where(c => c.UserID == user.Id)
                                      .ToListAsync();
            ViewBag.Cards = cards;

            return View(cart);
        }

        // POST: /Orders/Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(int orderId, int selectedCardId, string? couponCode)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var cart = await _context.Orders
                                     .Include(o => o.OrderDetails)
                                     .ThenInclude(od => od.Book)
                                     .FirstOrDefaultAsync(o => o.OrderID == orderId &&
                                                               o.UserID == user.Id &&
                                                               o.OrderStatus == "InCart");

            if (cart == null) return NotFound();

            if (!cart.OrderDetails.Any())
            {
                TempData["CartMessage"] = "You cannot check out with an empty cart.";
                return RedirectToAction("Cart");
            }

            // Validate card ownership
            var card = await _context.Cards
                                     .FirstOrDefaultAsync(c => c.CardID == selectedCardId &&
                                                               c.UserID == user.Id);
            if (card == null)
            {
                ModelState.AddModelError("", "You must select a valid payment card.");
            }

            // Subtotal BEFORE any coupon discount (used for free-shipping threshold)
            decimal subtotalBeforeCoupon = cart.OrderDetails.Sum(od => od.Quantity * od.Price);
            bool freeShipping = false;
            Coupon? coupon = null;

            //--------------------------------------------------------------
            // COUPON VALIDATION + APPLICATION
            //--------------------------------------------------------------
            if (!string.IsNullOrWhiteSpace(couponCode))
            {
                coupon = await _context.Coupons
                                       .FirstOrDefaultAsync(c => c.CouponCode == couponCode &&
                                                                 c.Status == "Enabled");

                if (coupon == null)
                {
                    ModelState.AddModelError("", "The coupon code is not valid or enabled.");
                }
                else
                {
                    // PERCENT OFF
                    if (coupon.CouponType == "PercentOff")
                    {
                        if (coupon.DiscountPercent == null || coupon.DiscountPercent <= 0)
                        {
                            ModelState.AddModelError("", "Invalid percent-off coupon.");
                        }
                        else
                        {
                            foreach (var detail in cart.OrderDetails)
                            {
                                decimal discount = detail.Price * (coupon.DiscountPercent.Value / 100m);
                                detail.Price -= discount;
                                detail.CouponID = coupon.CouponID;
                            }
                        }
                    }
                    // FREE SHIPPING
                    else if (coupon.CouponType == "FreeShipping")
                    {
                        decimal threshold = coupon.FreeThreshold ?? 0m;

                        if (subtotalBeforeCoupon >= threshold)
                        {
                            freeShipping = true;
                            // Tag coupon on each detail (optional, but lets you track usage)
                            foreach (var detail in cart.OrderDetails)
                            {
                                detail.CouponID = coupon.CouponID;
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("",
                                $"This coupon requires a minimum order of {threshold:c} to qualify for free shipping.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unknown coupon type.");
                    }
                }
            }

            // If there are any validation errors, recalc and redisplay
            if (!ModelState.IsValid)
            {
                var totalsError = ComputeCartTotals(cart, freeShipping);
                await _context.SaveChangesAsync();

                ViewBag.Subtotal = totalsError.subtotal;
                ViewBag.Shipping = totalsError.shipping;
                ViewBag.Total = totalsError.total;

                var cards = await _context.Cards
                                          .Where(c => c.UserID == user.Id)
                                          .ToListAsync();
                ViewBag.Cards = cards;

                return View(cart);
            }

            // Final totals (after coupon)
            var totals = ComputeCartTotals(cart, freeShipping);

            // Attach card to each order detail
            foreach (var detail in cart.OrderDetails)
            {
                detail.CardID = card.CardID;
            }

            // Decrease inventory for each book
            foreach (var detail in cart.OrderDetails)
            {
                var book = await _context.Books.FindAsync(detail.BookID);
                if (book != null)
                {
                    book.InventoryQuantity -= detail.Quantity;
                    if (book.InventoryQuantity < 0) book.InventoryQuantity = 0;
                }
            }

            cart.OrderDate = DateTime.Now;
            cart.OrderStatus = "Completed";

            await _context.SaveChangesAsync();

            return RedirectToAction("OrderConfirmation", new { id = cart.OrderID });
        }

        // ======================================
        // ORDER CONFIRMATION
        // ======================================

        // GET: /Orders/OrderConfirmation/5
        public async Task<IActionResult> OrderConfirmation(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var order = await _context.Orders
                                      .Include(o => o.OrderDetails)
                                      .ThenInclude(od => od.Book)
                                      .ThenInclude(b => b.Genre)
                                      .FirstOrDefaultAsync(o => o.OrderID == id &&
                                                                o.UserID == user.Id);

            if (order == null) return NotFound();

            var totals = ComputeStoredOrderTotals(order);
            ViewBag.Subtotal = totals.subtotal;
            ViewBag.Shipping = totals.shipping;
            ViewBag.Total = totals.total;

            // Simple recommendations:
            // three other active books in the same genre as the first purchased book
            var firstBook = order.OrderDetails.FirstOrDefault()?.Book;
            List<Book> recommendations = new();

            if (firstBook != null)
            {
                var purchasedIds = order.OrderDetails.Select(od => od.BookID).ToList();

                recommendations = await _context.Books
                    .Where(b => b.GenreID == firstBook.GenreID &&
                                b.BookStatus == "Active" &&
                                !purchasedIds.Contains(b.BookID))
                    .OrderBy(b => b.Title)
                    .Take(3)
                    .ToListAsync();
            }

            ViewBag.Recommendations = recommendations;

            return View(order);
        }
    }
}
