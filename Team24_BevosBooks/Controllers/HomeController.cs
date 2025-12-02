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
            // CHANGE: If user searched from Home, send them to full catalog page
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                return RedirectToAction("Index", "Books", new { searchString });
            }

            // Optional: keep lightweight data for future home widgets
            var books = await _context.Books
                .OrderBy(b => b.Title)
                .Take(6)
                .ToListAsync();

            return View(books);
        }
    }
}
