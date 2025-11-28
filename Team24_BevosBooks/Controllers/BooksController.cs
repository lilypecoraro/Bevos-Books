using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using static Team24_BevosBooks.Models.Order;

namespace Team24_BevosBooks.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BooksController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            IQueryable<Book> query = _context.Books.Include(b => b.Genre);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(b =>
                    b.Title.Contains(searchString) ||
                    b.Authors.Contains(searchString) ||
                    b.Genre.GenreName.Contains(searchString) ||
                    b.BookNumber.ToString().Contains(searchString));
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
                _ => query.OrderBy(b => b.Title),
            };

            ViewBag.GenreID = new SelectList(await _context.Genres.OrderBy(g => g.GenreName).ToListAsync(),
                                             "GenreID", "GenreName");

            ViewBag.SelectedGenreId = genreId ?? 0;
            ViewBag.SearchString = searchString;
            ViewBag.InStockOnly = inStockOnly;
            ViewBag.SortOrder = sortOrder;

            return View(await query.ToListAsync());
        }

        // =========================================================
        // DETAILS
        // =========================================================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var book = await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Reviews)
                    .ThenInclude(r => r.Reviewer)
                .FirstOrDefaultAsync(b => b.BookID == id);

            if (book == null)
                return NotFound();

            // Filter approved reviews only
            book.Reviews = book.Reviews
                .Where(r => r.DisputeStatus == "Approved")
                .ToList();

            // Current user
            var userId = _userManager.GetUserId(User);

            // ⭐ Count OTHER carts containing this book
            int cartsContainingBook = await _context.OrderDetails
                .Where(od => od.BookID == id)
                .Where(od => od.Order.OrderStatus == "InCart")       // FIXED
                .Where(od => od.Order.User.Id != userId)           // exclude current user's cart
                .CountAsync();

            // ⭐ Make it accessible to the view
            ViewBag.CartsContaining = cartsContainingBook;

            return View(book);
        }

        // =========================================================
        // CREATE (ADMIN)
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
            book.BookStatus = "Active";

            if (_context.Books.Any(b => b.BookNumber == book.BookNumber))
                ModelState.AddModelError("", "That Book Number already exists.");

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
            TempData["Message"] = $"Book '{book.Title}' created successfully.";

            return RedirectToAction(nameof(Index));
        }

        // =========================================================
        // EDIT (ADMIN)
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

            if (editedBook.InventoryQuantity < 0)
                ModelState.AddModelError("", "Inventory cannot be negative.");

            if (editedBook.ReorderPoint < 0)
                ModelState.AddModelError("", "Reorder Point cannot be negative.");

            if (!ModelState.IsValid)
            {
                await PopulateGenresDropDownList(editedBook.GenreID);
                return View(editedBook);
            }

            // Protect fields admins cannot change
            editedBook.BookNumber = originalBook.BookNumber;
            editedBook.Cost = originalBook.Cost;

            _context.Update(editedBook);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Book '{editedBook.Title}' updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // =========================================================
        // DISCONTINUE
        // =========================================================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Discontinue(int? id)
        {
            if (id == null) return NotFound();

            Book? book = await _context.Books
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.BookID == id);

            if (book == null) return NotFound();

            return View(book);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Discontinue")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DiscontinueConfirmed(int id)
        {
            Book? book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            book.BookStatus = "Discontinued";
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Book '{book.Title}' discontinued.";
            return RedirectToAction(nameof(Index));
        }

        // =========================================================
        // HELPERS
        // =========================================================
        private bool BookExists(int id) =>
            _context.Books.Any(e => e.BookID == id);

        private async Task PopulateGenresDropDownList(object? selectedGenre = null)
        {
            var genres = await _context.Genres.OrderBy(g => g.GenreName).ToListAsync();
            ViewBag.GenreID = new SelectList(genres, "GenreID", "GenreName", selectedGenre);
        }
    }
}