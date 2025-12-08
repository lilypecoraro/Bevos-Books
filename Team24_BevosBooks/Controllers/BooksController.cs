using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using Team24_BevosBooks.Services;
using Microsoft.AspNetCore.Http;
using Team24_BevosBooks.Extensions;   // ⭐ REQUIRED FOR Session.Get<T>() / Session.Set<T>()

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
            ViewBag.TotalCount = await _context.Books.CountAsync();

            IQueryable<Book> query = _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Reviews);

            if (!string.IsNullOrEmpty(searchString))
            {
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
                        .Where(od => od.BookID == b.BookID &&
                                     od.Order.OrderStatus == "Ordered")
                        .Sum(od => (int?)od.Quantity) ?? 0
                ),
                _ => query.OrderBy(b => b.Title),
            };

            ViewBag.GenreID = new SelectList(
                await _context.Genres.OrderBy(g => g.GenreName).ToListAsync(),
                "GenreID", "GenreName");

            ViewBag.SelectedGenreId = genreId ?? 0;
            ViewBag.SearchString = searchString;
            ViewBag.InStockOnly = inStockOnly;
            ViewBag.SortOrder = sortOrder;

            // Materialize current result set
            var books = await query.ToListAsync();

            // Batch-prefetch active discounts only for result set
            var bookIds = books.Select(b => b.BookID).ToList();
            var now = DateTime.Now;

            var discounts = await _context.ItemDiscounts
                .Where(d => bookIds.Contains(d.BookID) && d.Status == "Enabled")
                .Where(d =>
                    (!d.StartDate.HasValue || d.StartDate <= now) &&
                    (!d.EndDate.HasValue || d.EndDate >= now))
                .GroupBy(d => d.BookID)
                .Select(g => new
                {
                    BookID = g.Key,
                    // Prefer highest percent; if none, highest amount
                    BestPercent = g.Where(x => x.DiscountPercent.HasValue && x.DiscountPercent.Value > 0)
                                   .Max(x => (decimal?)x.DiscountPercent.Value),
                    BestAmount = g.Where(x => (!x.DiscountPercent.HasValue || x.DiscountPercent.Value <= 0) &&
                                          x.DiscountAmount.HasValue && x.DiscountAmount.Value > 0)
                                   .Max(x => (decimal?)x.DiscountAmount.Value)
                })
                .ToListAsync();

            var discMap = discounts.ToDictionary(x => x.BookID, x => x);
            var effectivePrices = new Dictionary<int, decimal>(books.Count);

            foreach (var b in books)
            {
                if (discMap.TryGetValue(b.BookID, out var info))
                {
                    if (info.BestPercent.HasValue && info.BestPercent.Value > 0)
                    {
                        var pct = info.BestPercent.Value / 100m;
                        var discounted = b.Price * (1 - pct);
                        effectivePrices[b.BookID] = discounted < 0 ? 0 : discounted;
                    }
                    else if (info.BestAmount.HasValue && info.BestAmount.Value > 0)
                    {
                        var discounted = b.Price - info.BestAmount.Value;
                        effectivePrices[b.BookID] = discounted < 0 ? 0 : discounted;
                    }
                    else
                    {
                        effectivePrices[b.BookID] = b.Price;
                    }
                }
                else
                {
                    effectivePrices[b.BookID] = b.Price;
                }
            }

            ViewBag.EffectivePrices = effectivePrices;

            return View(books);
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

            book.Reviews = book.Reviews
                .Where(r => r.DisputeStatus == "Approved")
                .ToList();

            // Compute effective price considering any active item discount
            var effectivePrice = await Pricing.GetEffectivePriceAsync(_context, book);
            ViewBag.EffectivePrice = effectivePrice;

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
                    .Where(o => o.UserID == userId && o.OrderStatus == "Ordered")
                    .AnyAsync(o => o.OrderDetails.Any(od => od.BookID == id));

                hasReviewed = await _context.Reviews
                    .AnyAsync(r => r.BookID == id && r.ReviewerID == userId);
            }

            ViewBag.CanReview = purchased && !hasReviewed;
            ViewBag.HasReviewed = hasReviewed;

            // ⭐ TRACK RECENTLY VIEWED USING SESSION EXTENSIONS
            List<int> viewed = HttpContext.Session.Get<List<int>>("RecentlyViewed") ?? new List<int>();

            if (!viewed.Contains(book.BookID))
            {
                viewed.Add(book.BookID);

                if (viewed.Count > 12)
                    viewed.RemoveAt(0);

                HttpContext.Session.Set("RecentlyViewed", viewed);
            }

            return View(book);
        }

        // =========================================================
        // CREATE
        // =========================================================
        [Authorize] // needs to be this way so request can hit the action and redirect if not properly authorized
        public async Task<IActionResult> Create()
        {

            if (!User.IsInRole("Admin"))
                return View("AccessDenied");

            await PopulateGenresDropDownList();
            return View();
            
           
        }

        [Authorize] // needs to be this way so request can hit the action and redirect if not properly authorized
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (!User.IsInRole("Admin"))
                return View("AccessDenied");
            
            // Set server-side fields
            book.BookStatus = "Active";

            ModelState.Remove(nameof(Book.BookStatus));
            ModelState.Remove(nameof(Book.Genre));
            ModelState.Remove(nameof(Book.Reviews));

            if (book.BookNumber == 0)
            {
                int currentMax = await _context.Books.AnyAsync()
                    ? await _context.Books.MaxAsync(b => b.BookNumber)
                    : 0;

                int nextNumber = Math.Max(currentMax + 1, 222301);
                book.BookNumber = nextNumber;
            }

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


        // EDIT Stashed changes
        // =========================================================
        [Authorize] // allow request to reach the action and redirect if not properly authorized
        public async Task<IActionResult> Edit(int? id)
        {
            // Redirect anyone who is not an Admin
            if (!User.IsInRole("Admin"))
                return View("AccessDenied");

            if (id == null) return NotFound();

            Book? book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            await PopulateGenresDropDownList(book.GenreID);
            return View(book);
        }

        // =========================================================
        // EDIT (POST)
        // =========================================================
        [Authorize] // allow request to reach the action and redirect if not properly authorized
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book editedBook)
        {
            // Redirect anyone who is not an Admin
            if (!User.IsInRole("Admin"))
                return View("AccessDenied");

            if (id != editedBook.BookID) return NotFound();

            Book? originalBook = await _context.Books.AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookID == id);

            if (originalBook == null) return NotFound();

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

            editedBook.BookNumber = originalBook.BookNumber;
            editedBook.Cost = originalBook.Cost;

            _context.Update(editedBook);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Book '{editedBook.Title}' updated.";
            return RedirectToAction(nameof(Index));
        }

        // =========================================================
        // DISCONTINUE
        // =========================================================
        [Authorize(Roles = "Admin")] // this is fine as is because this doesn't lead to a page -- Sean
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

            if (book.BookStatus != "Discontinued")
            {
                book.BookStatus = "Discontinued";
                await _context.SaveChangesAsync();

                TempData["Message"] = $"Book '{book.Title}' has been discontinued.";
                TempData["MessageType"] = "warning";

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
                            "));
                    }
                });
            }
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
        // GENRES
        // =========================================================
        private async Task PopulateGenresDropDownList(object? selectedGenre = null)
        {
            ViewBag.GenreID = new SelectList(
                await _context.Genres.OrderBy(g => g.GenreName).ToListAsync(),
                "GenreID", "GenreName", selectedGenre);
        }

        // =========================================================
        // DISCOVER
        // =========================================================
        public async Task<IActionResult> Discover()
        {
            // ⭐ GET RECENTLY VIEWED FIRST — VERY IMPORTANT
            var viewed = HttpContext.Session.Get<List<int>>("RecentlyViewed");

            if (viewed != null && viewed.Any())
            {
                int lastId = viewed.Last();
                var last = await _context.Books
                    .Include(b => b.Genre)
                    .FirstOrDefaultAsync(b => b.BookID == lastId);

                ViewBag.LastViewedBook = last;
            }
            else
            {
                ViewBag.LastViewedBook = null;
            }

            // ⭐ PERSONALIZED RECOMMENDATIONS
            var recs = await GetDiscoverRecommendations();
            ViewBag.Recommendations = recs;

            // ⭐ BESTSELLERS
            var bestsellers = await _context.Books
                .Include(b => b.Genre)
                .OrderByDescending(b =>
                    _context.OrderDetails
                        .Where(od => od.BookID == b.BookID &&
                                     od.Order.OrderStatus == "Ordered")
                        .Sum(od => (int?)od.Quantity) ?? 0)
                .Take(6)
                .ToListAsync();
            ViewBag.Bestsellers = bestsellers;

            // ⭐ GENRE SPOTLIGHTS
            var spotlightGenres = await _context.Genres
                .OrderBy(g => g.GenreName)
                .Take(3)
                .ToListAsync();

            var sections = new Dictionary<string, List<Book>>();

            foreach (var genre in spotlightGenres)
            {
                sections.Add(
                    genre.GenreName,
                    await _context.Books
                        .Where(b => b.GenreID == genre.GenreID &&
                                    b.BookStatus == "Active")
                        .OrderBy(b => b.Title)
                        .Take(4)
                        .ToListAsync()
                );
            }

            ViewBag.GenreSections = sections;

            return View();
        }
        // =========================================================
        // DISCOVER RECOMMENDATIONS
        // =========================================================
        private async Task<List<Book>> GetDiscoverRecommendations()
        {
            var viewed = HttpContext.Session.Get<List<int>>("RecentlyViewed");

            if (viewed == null || viewed.Count == 0)
                return new List<Book>();

            var viewedBooks = await _context.Books
                .Include(b => b.Genre)
                .Where(b => viewed.Contains(b.BookID))
                .ToListAsync();

            var genreIds = viewedBooks
                .Select(b => b.GenreID)
                .Distinct()
                .ToList();

            var recs = await _context.Books
    .Include(b => b.Reviews)   // ⭐ REQUIRED so EF can access Reviews
    .Where(b => genreIds.Contains(b.GenreID) && !viewed.Contains(b.BookID))
    .OrderByDescending(b =>
        b.Reviews.Any(r => r.DisputeStatus == "Approved")
            ? b.Reviews
                .Where(r => r.DisputeStatus == "Approved")
                .Average(r => r.Rating)
            : 0
    )
    .Take(8)
    .ToListAsync();
            return recs;
        }
    }
}