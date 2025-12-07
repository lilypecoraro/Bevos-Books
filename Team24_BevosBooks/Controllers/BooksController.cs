using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using Team24_BevosBooks.Services;

namespace Team24_BevosBooks.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;

        public BooksController(AppDbContext context,
                               UserManager<AppUser> userManager,
                               IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // =========================================================
        // INDEX
        // =========================================================
        public async Task<IActionResult> Index(
            string? searchString,
            int? genreId,
            bool inStockOnly = false,
            string sortOrder = "title")
        {
            // ⭐ Always set total count of all books in DB
            ViewBag.TotalCount = await _context.Books.CountAsync();

            // Include Reviews so the Index view can compute average ratings
            IQueryable<Book> query = _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Reviews);

            // Apply filters
            if (!string.IsNullOrEmpty(searchString))
            {
                // Split search string into individual words
                var keywords = searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var keyword in keywords)
                {
                    query = query.Where(b =>
                        b.Title.Contains(keyword) ||
                        b.Authors.Contains(keyword) ||
                        b.Genre.GenreName.Contains(keyword) ||
                        b.BookNumber.ToString().Contains(keyword));
                }
            }

            if (genreId.HasValue && genreId.Value != 0)
                query = query.Where(b => b.GenreID == genreId.Value);

            if (inStockOnly)
                query = query.Where(b => b.InventoryQuantity > 0);

            // Apply sorting
            query = sortOrder switch
            {
                "author" => query.OrderBy(b => b.Authors),
                "newest" => query.OrderByDescending(b => b.PublishDate),
                "oldest" => query.OrderBy(b => b.PublishDate),
                "priceAsc" => query.OrderBy(b => b.Price),
                "priceDesc" => query.OrderByDescending(b => b.Price),

                "rating" => query.OrderByDescending(b =>
                    b.Reviews.Any(r => r.DisputeStatus == "Approved")
                        ? b.Reviews.Where(r => r.DisputeStatus == "Approved").Average(r => r.Rating)
                        : 0
                ),

                "popularity" => query.OrderByDescending(b =>
                    _context.OrderDetails
                        .Where(od => od.BookID == b.BookID && od.Order.OrderStatus == "Completed")
                        .Sum(od => (int?)od.Quantity) ?? 0
                ),

                _ => query.OrderBy(b => b.Title),
            };

            // Populate dropdowns and viewbag values
            ViewBag.GenreID = new SelectList(
                await _context.Genres.OrderBy(g => g.GenreName).ToListAsync(),
                "GenreID", "GenreName");

            ViewBag.SelectedGenreId = genreId ?? 0;
            ViewBag.SearchString = searchString;
            ViewBag.InStockOnly = inStockOnly;
            ViewBag.SortOrder = sortOrder;

            // Return filtered list
            return View(await query.ToListAsync());
        }

        // =========================================================
        // DETAILS
        // =========================================================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Reviews)
                    .ThenInclude(r => r.Reviewer)
                .FirstOrDefaultAsync(b => b.BookID == id);

            if (book == null) return NotFound();

            // Approved reviews only
            book.Reviews = book.Reviews
                .Where(r => r.DisputeStatus == "Approved")
                .ToList();

            var userId = _userManager.GetUserId(User);

            // Count other carts containing this book
            int cartsContaining = await _context.OrderDetails
                .Where(od => od.BookID == id)
                .Where(od => od.Order.OrderStatus == "InCart")
                .Where(od => od.Order.UserID != userId)
                .CountAsync();

            ViewBag.CartsContaining = cartsContaining;

            bool purchased = false;
            bool hasReviewed = false;

            if (!string.IsNullOrEmpty(userId) && User.IsInRole("Customer"))
            {
                purchased = await _context.Orders
                    .Where(o => o.UserID == userId && o.OrderStatus == "Completed")
                    .AnyAsync(o => o.OrderDetails.Any(od => od.BookID == id));

                hasReviewed = await _context.Reviews
                    .AnyAsync(r => r.BookID == id && r.ReviewerID == userId);
            }

            ViewBag.CanReview = purchased && !hasReviewed;
            ViewBag.HasReviewed = hasReviewed;

            return View(book);
        }

        // =========================================================
        // CREATE
        // =========================================================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            await PopulateGenresDropDownList();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            // Set server-side fields
            book.BookStatus = "Active";

            // Ignore nav/server-set properties for validation
            ModelState.Remove(nameof(Book.BookStatus));
            ModelState.Remove(nameof(Book.Genre));
            ModelState.Remove(nameof(Book.Reviews));

            // Auto-assign BookNumber if not provided (0)
            if (book.BookNumber == 0)
            {
                // Find current max BookNumber; start at 222301 if no books or lower
                int currentMax = await _context.Books.AnyAsync()
                    ? await _context.Books.MaxAsync(b => b.BookNumber)
                    : 0;

                int nextNumber = Math.Max(currentMax + 1, 222301);
                book.BookNumber = nextNumber;
            }

            // Business rules
            if (await _context.Books.AnyAsync(b => b.BookNumber == book.BookNumber))
                ModelState.AddModelError("", "Book Number already exists.");

            if (book.InventoryQuantity < 0)
                ModelState.AddModelError("", "Inventory cannot be negative.");

            if (book.ReorderPoint < 0)
                ModelState.AddModelError("", "Reorder Point cannot be negative.");

            if (!ModelState.IsValid)
            {
                await PopulateGenresDropDownList(book.GenreID);
                return View(book);
            }

            _context.Add(book);
            await _context.SaveChangesAsync();
            TempData["Message"] = $"Book '{book.Title}' created.";

            return RedirectToAction(nameof(Index));
        }


        // =========================================================
        // EDIT (MERGED)
        // =========================================================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            Book? book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            await PopulateGenresDropDownList(book.GenreID);
            return View(book);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book editedBook)
        {
            if (id != editedBook.BookID) return NotFound();

            Book? originalBook = await _context.Books.AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookID == id);

            if (originalBook == null) return NotFound();

            // Combined safe model-state cleanup
            ModelState.Remove(nameof(Book.Genre));
            ModelState.Remove(nameof(Book.Reviews));
            ModelState.Remove(nameof(Book.BookNumber));
            ModelState.Remove(nameof(Book.Cost));

            if (editedBook.InventoryQuantity < 0)
                ModelState.AddModelError("", "Inventory cannot be negative.");

            if (editedBook.ReorderPoint < 0)
                ModelState.AddModelError("", "Reorder Point cannot be negative.");

            if (!ModelState.IsValid)
            {
                await PopulateGenresDropDownList(editedBook.GenreID);
                return View(editedBook);
            }

            // Preserve non-user-editable values
            editedBook.BookNumber = originalBook.BookNumber;
            editedBook.Cost = originalBook.Cost;

            _context.Update(editedBook);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Book '{editedBook.Title}' updated.";
            return RedirectToAction(nameof(Index));
        }

        // =========================================================
        // DISCONTINUE + EMAIL USERS (FULL COMBINED)
        // =========================================================
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Discontinue")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DiscontinueToggle(int id)
        {
            var book = await _context.Books
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.BookID == id);

            if (book == null)
            {
                TempData["Message"] = "Book not found.";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }

            // CASE 1: Book is Active → Discontinue it + send email
            if (book.BookStatus != "Discontinued")
            {
                book.BookStatus = "Discontinued";
                await _context.SaveChangesAsync();

                TempData["Message"] = $"Book '{book.Title}' has been discontinued.";
                TempData["MessageType"] = "warning";

                // Background email sending
                _ = Task.Run(async () =>
                {
                    var users = await _context.Users
                        .Where(u => u.Status == AppUser.UserStatus.Customer && u.Email != null)
                        .ToListAsync();

                    foreach (var user in users)
                    {
                        await _emailSender.SendEmailAsync(
                            user.Email,
                            "Team 24: Book Discontinued",
                            EmailTemplate.Wrap($@"
                        <h2>Book Discontinued</h2>
                        <p>Hello {user.FirstName},</p>
                        <p>The book <strong>{book.Title}</strong> by {book.Authors} is now discontinued.</p>
                        <p>Genre: {book.Genre?.GenreName}</p>
                        <p>Thank you for being part of Bevo's Books! 🤘</p>
                    ")
                        );
                    }
                });
            }
            // CASE 2: Book is Discontinued → Reactivate it (no email needed)
            else
            {
                book.BookStatus = "Active";
                await _context.SaveChangesAsync();

                TempData["Message"] = $"Book '{book.Title}' is now available again.";
                TempData["MessageType"] = "success";
            }

            return RedirectToAction("Details", new { id = book.BookID });
        }



        // =========================================================
        // GENRES DROPDOWN
        // =========================================================
        private async Task PopulateGenresDropDownList(object? selectedGenre = null)
        {
            ViewBag.GenreID = new SelectList(
                await _context.Genres.OrderBy(g => g.GenreName).ToListAsync(),
                "GenreID", "GenreName", selectedGenre);
        }

        // =========================================================
        // HOMECATALOG
        // =========================================================
        public async Task<IActionResult> HomeCatalog()
        {
            var bestsellers = await _context.Books
                .Include(b => b.Genre)
                .Where(b => b.BookStatus == "Active")
                .OrderByDescending(b =>
                    _context.OrderDetails
                        .Where(od => od.BookID == b.BookID &&
                                     od.Order.OrderStatus == "Completed")
                        .Sum(od => (int?)od.Quantity) ?? 0)
                .Take(6)
                .ToListAsync();

            var spotlightGenres = await _context.Genres
                .OrderBy(g => g.GenreName)
                .Take(3)
                .ToListAsync();

            var genreSections = new Dictionary<string, List<Book>>();

            foreach (var genre in spotlightGenres)
            {
                genreSections.Add(
                    genre.GenreName,
                    await _context.Books
                        .Where(b => b.GenreID == genre.GenreID &&
                                    b.BookStatus == "Active")
                        .OrderBy(b => b.Title)
                        .Take(4)
                        .ToListAsync()
                );
            }

            ViewBag.Bestsellers = bestsellers;
            ViewBag.GenreSections = genreSections;

            return View();
        }
    }
}