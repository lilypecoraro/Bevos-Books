using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public HomeController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // ----------------------------
        // LANDING PAGE (Books, Promo Marquee, Featured Coupon)
        // ----------------------------
        public async Task<IActionResult> Index(string? searchString)
        {
            // If user typed in search bar on Home → redirect to Books catalog
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                return RedirectToAction("Index", "Books", new { searchString });
            }

            // --- BOOKS FOR HOMEPAGE DISPLAY ---
            var books = await _context.Books
                .OrderBy(b => b.Title)
                .Take(6)
                .ToListAsync();

            // --- ENABLED COUPONS FOR MARQUEE ---
            var promos = await _context.Coupons
                .Where(c => c.Status == "Enabled")
                .ToListAsync();

            ViewBag.Promos = promos;

            // --- FEATURED COUPON FOR HOME PAGE ---
            int homeId = _config.GetValue<int>("HomeCouponId");  // chosen via Coupon page button

            var homeCoupon = await _context.Coupons
                .FirstOrDefaultAsync(c => c.CouponID == homeId && c.Status == "Enabled");

            ViewBag.HomeCoupon = homeCoupon;

            return View(books);
        }

        // ----------------------------
        // ABOUT US PAGE
        // ----------------------------
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}