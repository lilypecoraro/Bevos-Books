using Microsoft.AspNetCore.Mvc;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace Team24_BevosBooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // ----------------------------
        // LANDING PAGE (Requirements: welcome, promotions, search bar)
        // ----------------------------
        public async Task<IActionResult> Index(string? searchString)
        {
            // If user searched from Home, send them to full catalog page
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                return RedirectToAction("Index", "Books", new { searchString });
            }

            // Lightweight book data for homepage widgets
            var books = await _context.Books
                .OrderBy(b => b.Title)
                .Take(6)
                .ToListAsync();

            // Get enabled coupons for promo marquee
            var promos = await _context.Coupons
                .Where(c => c.Status == "Enabled")
                .ToListAsync();

            ViewBag.Promos = promos;

            return View(books);
        }

        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
