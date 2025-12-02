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

        // GET: Coupons (Public - anyone can view)
        public async Task<IActionResult> Index()
        {
            var coupons = await _context.Coupons
                .OrderByDescending(c => c.Status == "Enabled")
                .ThenBy(c => c.CouponCode)
                .ToListAsync();
            return View(coupons);
        }

        // =========================================================
        // CREATE COUPON (ADMIN ONLY)
        // =========================================================
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
            // Validate CouponCode is unique
            if (_context.Coupons.Any(c => c.CouponCode.ToUpper() == coupon.CouponCode.ToUpper()))
            {
                ModelState.AddModelError("CouponCode", "This coupon code already exists.");
            }

            // Validate CouponType-specific fields
            if (coupon.CouponType == "PercentOff")
            {
                if (!coupon.DiscountPercent.HasValue || coupon.DiscountPercent.Value <= 0)
                {
                    ModelState.AddModelError("DiscountPercent", "Discount Percent is required for PercentOff coupons.");
                }
                else if (coupon.DiscountPercent.Value > 100)
                {
                    ModelState.AddModelError("DiscountPercent", "Discount Percent cannot exceed 100%.");
                }

                // Clear FreeThreshold for PercentOff coupons
                coupon.FreeThreshold = null;
            }
            else if (coupon.CouponType == "FreeShipping")
            {
                if (!coupon.FreeThreshold.HasValue || coupon.FreeThreshold.Value <= 0)
                {
                    ModelState.AddModelError("FreeThreshold", "Free Threshold is required for FreeShipping coupons.");
                }

                // Clear DiscountPercent for FreeShipping coupons
                coupon.DiscountPercent = null;
            }
            else
            {
                ModelState.AddModelError("CouponType", "Invalid Coupon Type. Must be 'PercentOff' or 'FreeShipping'.");
            }

            if (!ModelState.IsValid)
            {
                return View(coupon);
            }

            // New coupons are Enabled by default
            coupon.Status = "Enabled";
            coupon.CouponCode = coupon.CouponCode.ToUpper(); // Standardize to uppercase

            _context.Add(coupon);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Coupon '{coupon.CouponCode}' created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // =========================================================
        // ENABLE COUPON (ADMIN ONLY)
        // =========================================================
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

        // =========================================================
        // DISABLE COUPON (ADMIN ONLY)
        // =========================================================
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

        // =========================================================
        // DETAILS (Public - for viewing coupon info)
        // =========================================================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var coupon = await _context.Coupons
                .Include(c => c.OrderDetails)
                .FirstOrDefaultAsync(c => c.CouponID == id);

            if (coupon == null) return NotFound();

            // Calculate usage count
            ViewBag.UsageCount = coupon.OrderDetails?.Count ?? 0;

            return View(coupon);
        }
    }
}