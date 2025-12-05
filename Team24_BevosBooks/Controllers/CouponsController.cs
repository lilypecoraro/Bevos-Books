using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Controllers
{
    public class CouponsController : Controller
    {
        private readonly AppDbContext _context;

        public CouponsController(AppDbContext context)
        {
            _context = context;
        }

        // Public list
        public async Task<IActionResult> Index()
        {
            var coupons = await _context.Coupons
                .OrderBy(c => c.CouponCode)
                .ToListAsync();

            return View(coupons);
        }

        // Create (admin)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Coupon coupon)
        {
            // Read the unbound checkbox
            bool freeShippingAllOrders = Request.Form["FreeShippingAllOrders"] == "on";

            // Set defaults before validation
            coupon.Status = "Enabled";
            coupon.OrderDetails = new List<OrderDetail>(); // initialize navigation property

            // Remove from ModelState so validation doesn’t block saving
            ModelState.Remove("Status");
            ModelState.Remove("OrderDetails");

            // Ensure unique code
            if (_context.Coupons.Any(c => c.CouponCode.ToUpper() == coupon.CouponCode.ToUpper()))
            {
                ModelState.AddModelError("CouponCode", "This coupon code already exists.");
            }

            if (coupon.CouponType == "PercentOff")
            {
                if (!coupon.DiscountPercent.HasValue || coupon.DiscountPercent.Value <= 0)
                {
                    ModelState.AddModelError("DiscountPercent", "Discount percent is required and must be greater than 0.");
                }
                else if (coupon.DiscountPercent.Value > 100)
                {
                    ModelState.AddModelError("DiscountPercent", "Discount percent cannot exceed 100.");
                }

                coupon.FreeThreshold = null; // not used for percent off
            }
            else if (coupon.CouponType == "FreeShipping")
            {
                if (freeShippingAllOrders)
                {
                    coupon.FreeThreshold = null; // treat as all orders
                }
                else if (!coupon.FreeThreshold.HasValue || coupon.FreeThreshold.Value <= 0)
                {
                    ModelState.AddModelError("FreeThreshold", "Enter a minimum order amount greater than 0, or tick 'Apply to all orders'.");
                }

                coupon.DiscountPercent = null; // not used for free shipping
            }
            else
            {
                ModelState.AddModelError("CouponType", "Invalid coupon type. Choose 'PercentOff' or 'FreeShipping'.");
            }

            if (!ModelState.IsValid)
            {
                // Preserve checkbox state for re-render
                ViewBag.FreeShippingAllOrders = freeShippingAllOrders;
                return View(coupon);
            }

            coupon.CouponCode = coupon.CouponCode.ToUpper();

            _context.Add(coupon);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Coupon '{coupon.CouponCode}' created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // Enable/Disable (admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Enable(int? id)
        {
            if (id == null) return NotFound();
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null) return NotFound();

            coupon.Status = "Enabled";
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Coupon '{coupon.CouponCode}' has been enabled.";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Disable(int? id)
        {
            if (id == null) return NotFound();
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null) return NotFound();

            coupon.Status = "Disabled";
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Coupon '{coupon.CouponCode}' has been disabled.";
            return RedirectToAction(nameof(Index));
        }

        // Details (public)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var coupon = await _context.Coupons
                .Include(c => c.OrderDetails)
                .FirstOrDefaultAsync(c => c.CouponID == id);

            if (coupon == null) return NotFound();

            ViewBag.UsageCount = coupon.OrderDetails?.Count ?? 0;
            return View(coupon);
        }
    }
}
