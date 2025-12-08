using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ReviewsController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var reviews = await _context.Reviews.ToListAsync();
            return View(reviews);
        }

        // Pending queue (Admin/Employee only)
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Pending()
        {
            var pendingReviews = _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.Reviewer)
                .Where(r => r.DisputeStatus == "Pending")
                .ToList();

            return View(pendingReviews);
        }

        // Approve review (Admin/Employee only)
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Approve(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound();

            var approverId = _userManager.GetUserId(User);
            review.DisputeStatus = "Approved";
            review.ApproverID = approverId;

            await _context.SaveChangesAsync();
            TempData["Message"] = "Review approved.";
            return RedirectToAction(nameof(Pending));
        }

        // Reject review (Admin/Employee only)
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Reject(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound();

            var approverId = _userManager.GetUserId(User);
            review.DisputeStatus = "Rejected";
            review.ApproverID = approverId;

            await _context.SaveChangesAsync();
            TempData["Message"] = "Review rejected.";
            return RedirectToAction(nameof(Pending));
        }

        // Admin-only: view all approved and rejected reviews
        [Authorize(Roles = "Admin")]
        public IActionResult Manage()
        {
            var moderated = _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.Reviewer)
                .Where(r => r.DisputeStatus == "Approved" || r.DisputeStatus == "Rejected")
                .OrderByDescending(r => r.ReviewID)
                .ToList();

            return View(moderated);
        }

        // ===============================
        // WRITE REVIEW
        // ===============================
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Submit(int bookId)
        {

            if (!User.IsInRole("Customer"))
                return View("AccessDeniedEmployee");

            // Load book details
            var book = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookID == bookId);

            if (book == null) return NotFound();

            // Prefill model with BookID; ReviewerID set on POST
            var model = new Review
            {
                BookID = bookId
            };

            // Pass book info to the view
            ViewBag.BookTitle = book.Title;
            ViewBag.BookAuthors = book.Authors;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(Review review)
        {

            if (!User.IsInRole("Customer"))
                return View("AccessDeniedEmployee");

            // Basic validation: book must exist
            var bookExists = await _context.Books.AnyAsync(b => b.BookID == review.BookID);
            if (!bookExists) return NotFound();

            // Ensure the current user is valid
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            // Ignore nav/server-set properties for validation
            ModelState.Remove(nameof(Review.Book));
            ModelState.Remove(nameof(Review.Reviewer));
            ModelState.Remove(nameof(Review.ReviewerID));

            // Enforce purchase requirement and no duplicate reviews
            var purchased = await _context.Orders
                .Where(o => o.UserID == userId && o.OrderStatus == "Ordered")
                .AnyAsync(o => o.OrderDetails.Any(od => od.BookID == review.BookID));

            var hasReviewed = await _context.Reviews
                .AnyAsync(r => r.BookID == review.BookID && r.ReviewerID == userId);

            if (!purchased)
            {
                ModelState.AddModelError("", "You can only review books you have purchased.");
            }
            if (hasReviewed)
            {
                ModelState.AddModelError("", "You have already reviewed this book.");
            }

            if (!ModelState.IsValid)
            {
                // Reload book info for the view
                var book = await _context.Books
                    .AsNoTracking()
                    .FirstOrDefaultAsync(b => b.BookID == review.BookID);

                if (book != null)
                {
                    ViewBag.BookTitle = book.Title;
                    ViewBag.BookAuthors = book.Authors;
                }

                return View(review);
            }

            // Set server-side fields
            review.ReviewerID = userId;
            review.DisputeStatus = "Pending";
            review.ApproverID = null;

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Your review was submitted and is pending approval.";
            return RedirectToAction("Details", "Books", new { id = review.BookID });
        }
        // ============================================================
        // EDIT REVIEW TEXT (Admin/Employee only)
        // ============================================================
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var review = await _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.Reviewer)
                .FirstOrDefaultAsync(r => r.ReviewID == id);

            if (review == null) return NotFound();

            return View(review);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Review edited)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound();

            // Only allow editing of text, not rating
            review.ReviewText = edited.ReviewText;

            await _context.SaveChangesAsync();
            TempData["Message"] = "Review text updated.";
            return RedirectToAction(nameof(Pending));
        }

    }
}
