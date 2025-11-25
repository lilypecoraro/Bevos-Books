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
            IQueryable<Book> query = _context.Books;

            // optional search
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(b =>
                    b.Title.Contains(searchString) ||
                    b.Authors.Contains(searchString));
            }

            // Home page shows 6 books max (not required, but clean)
            var books = await query
                .OrderBy(b => b.Title)
                .Take(6)
                .ToListAsync();

            return View(books);
        }
    }
}
