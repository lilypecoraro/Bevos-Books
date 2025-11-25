using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        // -------------------------------------------------
        // INDEX: Search, filter, sort
        // -------------------------------------------------
        public async Task<IActionResult> Index(
            string? searchString,
            int? genreId,
            bool inStockOnly = false,
            string sortOrder = "title")
        {
            // Base query with Genre
            IQueryable<Book> query = _context.Books
                                             .Include(b => b.Genre);

            // Search: title, author(s), genre name, book number
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(b =>
                    b.Title.Contains(searchString) ||
                    b.Authors.Contains(searchString) ||
                    b.Genre.GenreName.Contains(searchString) ||
                    b.BookNumber.ToString().Contains(searchString));
            }

            // Filter by genre (optional)
            if (genreId.HasValue && genreId.Value != 0)
            {
                query = query.Where(b => b.GenreID == genreId.Value);
            }

            // Filter by in-stock only
            if (inStockOnly)
            {
                query = query.Where(b => b.InventoryQuantity > 0);
            }

            // Sorting
            // Note: “popularity” / “highest rated” would use review data.
            // For now, they map to reasonable existing fields.
            query = sortOrder switch
            {
                "author" => query.OrderBy(b => b.Authors),
                "newest" => query.OrderByDescending(b => b.PublishDate),
                "oldest" => query.OrderBy(b => b.PublishDate),
                "priceAsc" => query.OrderBy(b => b.Price),
                "priceDesc" => query.OrderByDescending(b => b.Price),
                "popularity" => query.OrderBy(b => b.Title),        // placeholder
                "highestRated" => query.OrderBy(b => b.Title),       // placeholder
                _ => query.OrderBy(b => b.Title),
            };

            // Pass genres for dropdown
            ViewBag.GenreID = new SelectList(await _context.Genres
                                                           .OrderBy(g => g.GenreName)
                                                           .ToListAsync(),
                                             "GenreID",
                                             "GenreName");
            ViewBag.SelectedGenreId = genreId ?? 0;
            ViewBag.SearchString = searchString;
            ViewBag.InStockOnly = inStockOnly;
            ViewBag.SortOrder = sortOrder;

            var books = await query.ToListAsync();
            return View(books);
        }

        // -------------------------------------------------
        // DETAILS: Book info + reviews + add-to-cart link
        // -------------------------------------------------
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                                     .Include(b => b.Genre)
                                     .FirstOrDefaultAsync(b => b.BookID == id);

            if (book == null) return NotFound();

            return View(book);
        }

        // -------------------------------------------------
        // CREATE (ADMIN ONLY)
        // -------------------------------------------------
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
            if (!ModelState.IsValid)
            {
                await PopulateGenresDropDownList(book.GenreID);
                return View(book);
            }

            // New books start as Active by default
            if (string.IsNullOrEmpty(book.BookStatus))
            {
                book.BookStatus = "Active";
            }

            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // -------------------------------------------------
        // EDIT (ADMIN ONLY)
        // -------------------------------------------------
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            await PopulateGenresDropDownList(book.GenreID);
            return View(book);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.BookID) return NotFound();

            if (!ModelState.IsValid)
            {
                await PopulateGenresDropDownList(book.GenreID);
                return View(book);
            }

            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.BookID))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // -------------------------------------------------
        // DISCONTINUE (ADMIN ONLY) – mark inactive
        // -------------------------------------------------
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Discontinue(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
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
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            book.BookStatus = "Discontinued";
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // -------------------------------------------------
        // Helpers
        // -------------------------------------------------
        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }

        private async Task PopulateGenresDropDownList(object? selectedGenre = null)
        {
            var genresQuery = _context.Genres
                                      .OrderBy(g => g.GenreName);

            ViewBag.GenreID = new SelectList(await genresQuery.ToListAsync(),
                                             "GenreID",
                                             "GenreName",
                                             selectedGenre);
        }
    }
}
