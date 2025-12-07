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
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public CouponsController(AppDbContext context, IConfiguration config, IWebHostEnvironment env)
        {
            _context = context;
            _config = config;
            _env = env;
        }

        // Public list
        public async Task<IActionResult> Index()
        {
            var coupons = await _context.Coupons
                .OrderBy(c => c.CouponCode)
                .ToListAsync();

            return View(coupons);
        }

        // ------------------------------
        // ⭐ SET HOMEPAGE COUPON BUTTON
        // ------------------------------
        [Authorize(Roles = "Admin")]
        public IActionResult SetHomeCoupon(int id)
        {
            string jsonPath = Path.Combine(_env.ContentRootPath, "appsettings.json");

            var json = System.IO.File.ReadAllText(jsonPath);
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            obj["HomeCouponId"] = id;

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(jsonPath, output);

            TempData["Message"] = "Homepage coupon updated successfully!";
            return RedirectToAction(nameof(Index));
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
            bool freeShippingAllOrders = Request.Form["FreeShippingAllOrders"] == "on";

            coupon.Status = "Enabled";
            coupon.OrderDetails = new List<OrderDetail>();

            ModelState.Remove("Status");
            ModelState.Remove("OrderDetails");

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

                coupon.FreeThreshold = null;
            }
            else if (coupon.CouponType == "FreeShipping")
            {
                if (freeShippingAllOrders)
                {
                    coupon.FreeThreshold = null;
                }
                else if (!coupon.FreeThreshold.HasValue || coupon.FreeThreshold.Value <= 0)
                {
                    ModelState.AddModelError("FreeThreshold", "Enter a minimum order amount greater than 0, or select apply to all orders.");
                }

                coupon.DiscountPercent = null;
            }

            if (!ModelState.IsValid)
            {
                ViewBag.FreeShippingAllOrders = freeShippingAllOrders;
                return View(coupon);
            }

            coupon.CouponCode = coupon.CouponCode.ToUpper();

            _context.Add(coupon);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Coupon '{coupon.CouponCode}' created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // Enable/Disable
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

        // Details
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