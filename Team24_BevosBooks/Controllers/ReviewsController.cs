using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;

namespace Team24_BevosBooks.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly AppDbContext _context;

        public ReviewsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var reviews = await _context.Reviews.ToListAsync();
            return View(reviews);
        }

        public IActionResult Pending()
        {
            var pendingReviews = _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.Reviewer)
                .Where(r => r.DisputeStatus == "Pending")
                .ToList();

            return View(pendingReviews);
        }

    }
}