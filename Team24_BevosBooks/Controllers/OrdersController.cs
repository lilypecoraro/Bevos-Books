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
    [Authorize]  // Controller requires login — specific roles added per action
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

        // ========================================================
        // GET OR CREATE CART
        // ========================================================
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

        private async Task<List<string>> UpdateCartForChanges(Order cart)
        {
            List<string> messages = new();

            var items = cart.OrderDetails.ToList();
            foreach (var item in items)
            {
                var book = await _context.Books.FindAsync(item.BookID);

                if (book == null || book.BookStatus == "Discontinued")
                {
                    messages.Add($"'{item.Book?.Title}' is no longer available and was removed.");
                    _context.OrderDetails.Remove(item);
                    continue;
                }

                if (book.InventoryQuantity == 0)
                {
                    messages.Add($"'{book.Title}' is out of stock and was removed.");
                    _context.OrderDetails.Remove(item);
                    continue;
                }

                if (item.Quantity > book.InventoryQuantity)
                {
                    item.Quantity = book.InventoryQuantity;
                    messages.Add($"Quantity for '{book.Title}' reduced to {book.InventoryQuantity}.");
                }

                item.Price = book.Price;
                item.Cost = book.Cost;
            }

            await _context.SaveChangesAsync();
            return messages;
        }

        private (decimal subtotal, decimal shipping, decimal total)
            ComputeCartTotals(Order cart, bool freeShipping = false)
        {
            decimal subtotal = cart.OrderDetails.Sum(od => od.Price * od.Quantity);
            int qty = cart.OrderDetails.Sum(od => od.Quantity);

            decimal shipping = 0;
            if (!freeShipping && qty > 0)
            {
                shipping = FIRST_BOOK_SHIPPING + (qty - 1) * ADDITIONAL_BOOK_SHIPPING;
            }

            return (subtotal, shipping, subtotal + shipping);
        }

        // ========================================================
        // CART PAGE (CUSTOMER)
        // ========================================================
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

        // ========================================================
        // ADD TO CART (CUSTOMER)
        // ========================================================
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

        // ========================================================
        // REMOVE ITEM (CUSTOMER)
        // ========================================================
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

        // ========================================================
        // UPDATE QUANTITY (CUSTOMER)
        // ========================================================
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

        // ========================================================
        // CHECKOUT (GET)
        // ========================================================
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
            var totals = ComputeCartTotals(cart);

            var vm = new CheckoutViewModel
            {
                Order = cart,
                Cards = await _context.Cards.Where(c => c.UserID == user.Id).ToListAsync(),
                SelectedCardID = null,
                CouponCode = "",
                Subtotal = totals.subtotal,
                Shipping = totals.shipping,
                Total = totals.total
            };

            return View(vm);
        }

        // ========================================================
        // CHECKOUT (POST)
        // ========================================================
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Checkout(int orderId, int? selectedCardId, string? couponCode)
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
                return RedirectToAction("Cart");

            if (selectedCardId == null)
            {
                TempData["CheckoutMessage"] = "Please select a card.";
                return RedirectToAction("Checkout");
            }

            var card = await _context.Cards
                .FirstOrDefaultAsync(c => c.CardID == selectedCardId &&
                                          c.UserID == user.Id);

            if (card == null)
            {
                TempData["CheckoutMessage"] = "Invalid card.";
                return RedirectToAction("Checkout");
            }

            bool freeShipping = false;

            // ============= APPLY COUPON =========================
            if (!string.IsNullOrWhiteSpace(couponCode))
            {
                var coupon = await _context.Coupons
                    .FirstOrDefaultAsync(c => c.CouponCode == couponCode &&
                                              c.Status == "Enabled");

                if (coupon != null)
                {
                    decimal before = cart.OrderDetails.Sum(od => od.Price * od.Quantity);

                    if (coupon.CouponType == "PercentOff")
                    {
                        foreach (var d in cart.OrderDetails)
                        {
                            decimal discount = d.Price * (coupon.DiscountPercent.Value / 100m);
                            d.Price -= discount;
                        }
                    }
                    else if (coupon.CouponType == "FreeShipping")
                    {
                        if (before >= coupon.FreeThreshold)
                            freeShipping = true;
                        else
                            TempData["CheckoutMessage"] = $"This coupon requires {coupon.FreeThreshold:c}.";
                    }
                }
                else
                {
                    TempData["CheckoutMessage"] = "Invalid coupon.";
                }
            }

            var totals = ComputeCartTotals(cart, freeShipping);

            // DECREASE INVENTORY
            foreach (var item in cart.OrderDetails)
            {
                var book = await _context.Books.FindAsync(item.BookID);
                if (book != null)
                    book.InventoryQuantity -= item.Quantity;
            }

            // FINALIZE ORDER
            cart.OrderStatus = "Completed";
            cart.OrderDate = DateTime.Now;
            cart.ShippingFee = totals.shipping;

            foreach (var d in cart.OrderDetails)
                d.CardID = selectedCardId.Value;

            await _context.SaveChangesAsync();

            return RedirectToAction("OrderConfirmation", new { id = cart.OrderID });
        }

        // ========================================================
        // ORDER CONFIRMATION
        // ========================================================
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

            if (order == null) return NotFound();

            var firstBook = order.OrderDetails.First().Book;

            var recs = await _context.Books
                .Where(b => b.GenreID == firstBook.GenreID &&
                            b.BookStatus == "Active" &&
                            !order.OrderDetails.Select(od => od.BookID).Contains(b.BookID))
                .Take(3)
                .ToListAsync();

            ViewBag.Recommendations = recs;

            return View(order);
        }

        // ========================================================
        // ORDER HISTORY (CUSTOMER)
        // ========================================================
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

        // ========================================================
        // ALL ORDERS (ADMIN + EMPLOYEE)
        // ========================================================
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
