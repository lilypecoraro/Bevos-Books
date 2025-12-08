using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using Team24_BevosBooks.Models.ViewModels;
using Team24_BevosBooks.Services;
using Team24_BevosBooks.Services; // ensure Pricing is available

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
                // Determine next OrderID manually (Option 2)
                int nextOrderId = 1;

                if (_context.Orders.Any())
                {
                    nextOrderId = _context.Orders.Max(o => o.OrderID) + 1;
                }

                order = new Order
                {
                    OrderID = nextOrderId,    // ⭐ manually assign new incremental OrderID
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

                // UPDATED: use effective (discounted) price
                var effectivePrice = await Pricing.GetEffectivePriceAsync(_context, book);
                item.Price = effectivePrice;
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
            if (user == null) return RedirectToAction("Login", "Account");

            var cart = await GetOrCreateCart(user.Id);

            cart = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                .FirstAsync(o => o.OrderID == cart.OrderID);

            var messages = await UpdateCartForChanges(cart);
            var totals = await ComputeCartTotals(cart);

            // NEW: collect discounted item names
            var discountedItems = cart.OrderDetails
                .Where(od => od.Book != null && od.Price < od.Book.Price)
                .Select(od => od.Book.Title)
                .Distinct()
                .ToList();

            ViewBag.Messages = messages;
            ViewBag.Subtotal = totals.subtotal;
            ViewBag.Shipping = totals.shipping;
            ViewBag.Total = totals.total;
            ViewBag.DiscountedItems = discountedItems; // NEW

            return View(cart);
        }

        // ============================================================
        // ADD TO CART 
        // ============================================================
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
                .FirstOrDefaultAsync(od => od.OrderID == cart.OrderID && od.BookID == id);

            if (existing == null)
            {
                var effectivePrice = await Pricing.GetEffectivePriceAsync(_context, book); // UPDATED

                var newDetail = new OrderDetail
                {
                    OrderID = cart.OrderID,
                    Order = cart,
                    BookID = id,
                    Book = book,
                    Quantity = 1,
                    Price = effectivePrice, // UPDATED
                    Cost = book.Cost
                };

                _context.OrderDetails.Add(newDetail);
                await _context.SaveChangesAsync();

                TempData["CartMessage"] = $"'{book.Title}' was added to your cart.";
            }
            else
            {
                // Do not increment quantity — just show message
                TempData["CartMessage"] = $"'{book.Title}' is already in your cart.";
            }

            // Redirect to the cart page so the message shows there
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

            if (detail == null) return NotFound();

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

            // UPDATED: refresh to effective price
            var effectivePrice = await Pricing.GetEffectivePriceAsync(_context, detail.Book);
            detail.Price = effectivePrice;

            await _context.SaveChangesAsync();
            return RedirectToAction("Cart");
        }

        // ============================================================
        // ORDER HISTORY
        // ============================================================
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> History()
        {
            string userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var orders = await _context.Orders
                .Where(o => o.UserID == userId && o.OrderStatus == "Ordered") // only completed orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Card)
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

            var totals = await ComputeCartTotals(cart);

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

            // If coupon was applied, override totals
            if (TempData.ContainsKey("AppliedCoupon"))
            {
                vm.CouponCode = TempData["AppliedCoupon"]?.ToString();

                vm.OriginalSubtotal = decimal.Parse(TempData["OriginalSubtotal"]!.ToString());
                vm.Discount = decimal.Parse(TempData["DiscountAmount"]!.ToString());
                vm.DiscountedSubtotal = decimal.Parse(TempData["DiscountedSubtotal"]!.ToString());

                vm.Shipping = decimal.Parse(TempData["DiscountedShipping"]!.ToString());
                vm.Total = decimal.Parse(TempData["DiscountedTotal"]!.ToString());

                ViewBag.CheckoutMessage = TempData["CheckoutMessage"];
            }


            if (TempData.ContainsKey("CheckoutError"))
            {
                ViewBag.CheckoutError = TempData["CheckoutError"];
            }

            // NEW: collect discounted item names
            ViewBag.DiscountedItems = cart.OrderDetails
                .Where(od => od.Book != null && od.Price < od.Book.Price)
                .Select(od => od.Book.Title)
                .Distinct()
                .ToList();

            return View(vm);
        }

        // ============================================================
        // APPLY COUPON (POST) — DOES NOT CHECK OUT
        // ============================================================
        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyCoupon(CheckoutViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var cart = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Book)
                .FirstOrDefaultAsync(o => o.UserID == user.Id && o.OrderStatus == "InCart");

            if (cart == null || !cart.OrderDetails.Any())
            {
                TempData["CheckoutError"] = "Your cart is empty.";
                return RedirectToAction("Checkout");
            }

            // Reset all items to effective (discounted) price and clear coupon
            decimal effectiveSubtotalBeforeCoupon = 0m;
            foreach (var od in cart.OrderDetails)
            {
                var effectivePrice = await Pricing.GetEffectivePriceAsync(_context, od.Book);
                od.Price = effectivePrice;
                od.CouponID = null;
                effectiveSubtotalBeforeCoupon += effectivePrice * od.Quantity;
            }

            if (string.IsNullOrWhiteSpace(model.CouponCode))
            {
                TempData["CheckoutError"] = "Please enter a coupon code.";
                await _context.SaveChangesAsync();
                return RedirectToAction("Checkout");
            }

            string code = model.CouponCode.Trim().ToUpper();

            var coupon = await _context.Coupons
                .FirstOrDefaultAsync(c => c.CouponCode == code && c.Status == "Enabled");

            if (coupon == null)
            {
                TempData["CheckoutError"] = "Invalid or disabled coupon.";
                return RedirectToAction("Checkout");
            }

            // Check if customer already used it
            bool alreadyUsed = await _context.OrderDetails
                .Include(od => od.Order)
                .AnyAsync(od => od.Order.UserID == user.Id &&
                                od.Order.OrderStatus == "Ordered" &&
                                od.CouponID == coupon.CouponID);

            if (alreadyUsed)
            {
                TempData["CheckoutError"] = "You have already used this coupon.";
                return RedirectToAction("Checkout");
            }

            // Apply coupon on top of effective prices
            bool freeShipping = false;

            if (coupon.CouponType == "PercentOff")
            {
                foreach (var od in cart.OrderDetails)
                {
                    decimal discount = od.Price * (coupon.DiscountPercent!.Value / 100m);
                    od.Price -= discount;
                    od.CouponID = coupon.CouponID;
                }
            }
            else if (coupon.CouponType == "FreeShipping")
            {
                decimal subtotal = cart.OrderDetails.Sum(od => od.Price * od.Quantity);
                if (coupon.FreeThreshold == null || subtotal >= coupon.FreeThreshold)
                {
                    freeShipping = true;
                    foreach (var od in cart.OrderDetails)
                    {
                        od.CouponID = coupon.CouponID;
                    }
                }
                else
                {
                    TempData["CheckoutError"] =
                        $"This coupon requires a minimum subtotal of {coupon.FreeThreshold:c}.";
                    return RedirectToAction("Checkout");
                }
            }

            await _context.SaveChangesAsync();

            // Recompute totals after coupon
            var totals = await ComputeCartTotals(cart, freeShipping);

            // Use effective subtotal before coupon as "Original Subtotal"
            decimal originalSubtotal = effectiveSubtotalBeforeCoupon;
            decimal discountAmount = originalSubtotal - totals.subtotal;

            TempData["AppliedCoupon"] = code;
            TempData["OriginalSubtotal"] = originalSubtotal.ToString();
            TempData["DiscountedSubtotal"] = totals.subtotal.ToString();
            TempData["DiscountAmount"] = discountAmount.ToString();
            TempData["DiscountedShipping"] = totals.shipping.ToString();
            TempData["DiscountedTotal"] = totals.total.ToString();
            TempData["CheckoutMessage"] = $"Coupon '{code}' applied successfully!";



            TempData["CheckoutMessage"] = $"Coupon '{code}' applied successfully!";

            return RedirectToAction("Checkout");
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
            if (user == null) return RedirectToAction("Login", "Account");

            // Coupon is optional
            ModelState.Remove(nameof(CheckoutViewModel.CouponCode));

            // Always re-fetch cart from DB based on user + InCart
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
                                        od.Order.OrderStatus == "Ordered" &&
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

            // Reset prices to book price and clear coupon
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
                        decimal discount = od.Price * (coupon.DiscountPercent!.Value / 100m);
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

            var totals = await ComputeCartTotals(cart, freeShipping);
            model.Subtotal = cart.OrderDetails.Sum(od => od.Price * od.Quantity);
            decimal originalBeforeCheckout = cart.OrderDetails
    .Sum(od => od.Book.Price * od.Quantity);

            model.Discount = originalBeforeCheckout - model.Subtotal;

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

            // Deduct inventory
            foreach (var item in cart.OrderDetails)
            {
                var book = await _context.Books.FindAsync(item.BookID);
                if (book != null)
                {
                    book.InventoryQuantity -= item.Quantity;
                    if (book.InventoryQuantity < 0)
                        book.InventoryQuantity = 0;
                }
            }

            // Finalize order
            cart.OrderStatus = "Ordered";
            cart.OrderDate = DateTime.Now;
            cart.ShippingFee = totals.shipping;
            cart.CouponID = couponApplied && coupon != null ? coupon.CouponID : null;

            foreach (var od in cart.OrderDetails)
            {
                od.CardID = card.CardID;
            }

            await _context.SaveChangesAsync();

            // ==============================================
            // EMAIL: ORDER CONFIRMATION
            // ==============================================
            string itemHtml = string.Join("",
                cart.OrderDetails.Select(od =>
                    $"<li>{od.Book.Title} — {od.Quantity} × {od.Price:C}</li>"
                ));

            string emailBody = EmailTemplate.Wrap($@"
                <h2>Your Order Is Confirmed!</h2>

                <p>Hello {user.FirstName},</p>

                <p>Your order <strong>#{cart.OrderID}</strong> has been successfully placed on {cart.OrderDate:g}.</p>

                <h3>Order Details</h3>
                <ul>
                    {itemHtml}
                </ul>

                <p><strong>Subtotal:</strong> {model.Subtotal:C}<br/>
                   <strong>Shipping:</strong> {model.Shipping:C}<br/>
                   <strong>Total:</strong> {model.Total:C}</p>

                <p>Thank you for shopping at Bevo's Books! 🤘</p>
                <p><i>Team 24 — Bevo's Books</i></p>
            ");

            await _emailSender.SendEmailAsync(
                user.Email,
                "Team 24: Order Confirmation",
                emailBody
            );

            return RedirectToAction("OrderConfirmation", new { id = cart.OrderID });
        }
        // ============================================================
        // ORDER CONFIRMATION + RECOMMENDATIONS
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

            // ============================================================
            // RECOMMENDATIONS LOGIC — enforce specs in hierarchy
            // ============================================================

            // Helper: compute average approved rating (safe in memory)
            decimal AvgApproved(Book b) =>
                b.Reviews != null && b.Reviews.Any(r => r.DisputeStatus == "Approved")
                    ? (decimal)b.Reviews.Where(r => r.DisputeStatus == "Approved").Average(r => r.Rating)
                    : 0m;

            // SPEC: If multiple books in cart, only give recommendations for one
            var firstDetailWithBook = order.OrderDetails.FirstOrDefault(od => od.Book != null);
            List<Book> recs = new List<Book>();
            if (firstDetailWithBook == null)
            {
                ViewBag.AssignedRecommendations = recs;
                return View(order);
            }

            // SPEC: Do not recommend books already purchased (trumps all)
            var purchasedBookIds = await _context.Orders
                .Where(o => o.UserID == order.UserID)
                .SelectMany(o => o.OrderDetails.Select(od => od.BookID))
                .Distinct()
                .ToListAsync();

            var focusBookId = firstDetailWithBook.Book.BookID;
            var focusBook = await _context.Books
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(b => b.BookID == focusBookId);

            string focusAuthor = focusBook.Authors;
            int focusGenreId = focusBook.GenreID;

            // SPEC: Author’s other book in same genre (highest rated)
            var authorCandidates = await _context.Books
                .Include(b => b.Reviews)
                .Where(b => b.Authors == focusAuthor
                         && b.GenreID == focusGenreId
                         && b.BookID != focusBook.BookID
                         && b.BookStatus == "Active"
                         && !purchasedBookIds.Contains(b.BookID))
                .ToListAsync(); // materialize first

            var authorRec = authorCandidates
                .OrderByDescending(b => AvgApproved(b))
                .FirstOrDefault();

            if (authorRec != null)
            {
                recs.Add(authorRec);
            }

            // “Category below”: highly‑rated books of same genre, distinct authors
            var sameGenrePool = await _context.Books
                .Include(b => b.Reviews)
                .Where(b => b.GenreID == focusGenreId
                         && b.BookStatus == "Active"
                         && b.BookID != focusBook.BookID
                         && !purchasedBookIds.Contains(b.BookID))
                .ToListAsync(); // materialize first

            var highRatedDistinctAuthors = sameGenrePool
                .Where(b => AvgApproved(b) >= 4.0m)
                .OrderByDescending(b => AvgApproved(b))
                .GroupBy(b => b.Authors)
                .Select(g => g.First())
                .Take(2)
                .ToList();

            foreach (var hr in highRatedDistinctAuthors)
            {
                if (!recs.Any(r => r.BookID == hr.BookID))
                    recs.Add(hr);
            }

            // Fill blanks with low/no‑rated same‑genre books
            if (recs.Count < 3)
            {
                var fillers = sameGenrePool
                    .OrderByDescending(b => AvgApproved(b))
                    .Where(b => !recs.Any(r => r.BookID == b.BookID))
                    .Take(3 - recs.Count)
                    .ToList();

                recs.AddRange(fillers);

                // Relaxed second pass if still short
                if (recs.Count < 3)
                {
                    var relaxedFillers = sameGenrePool
                        .OrderByDescending(b => AvgApproved(b))
                        .Where(b => !recs.Any(r => r.BookID == b.BookID))
                        .Take(3 - recs.Count)
                        .ToList();

                    recs.AddRange(relaxedFillers);
                }
            }

            // If no genre books, fallback to highest rated overall
            if (recs.Count == 0 || (recs.Count < 3 && !sameGenrePool.Any()))
            {
                var overallCandidates = await _context.Books
                    .Include(b => b.Reviews)
                    .Where(b => b.BookStatus == "Active"
                             && b.BookID != focusBook.BookID
                             && !purchasedBookIds.Contains(b.BookID))
                    .ToListAsync(); // materialize first

                var overallFillers = overallCandidates
                    .OrderByDescending(b => AvgApproved(b))
                    .Take(3 - recs.Count)
                    .ToList();

                recs.AddRange(overallFillers);
            }

            // SPEC: Recommendations should include three books whenever possible
            ViewBag.AssignedRecommendations = recs.Take(3).ToList();
            return View(order);
        }

    }
}
