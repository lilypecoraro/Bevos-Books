// WORK-IN-PROGESS -- 11/18/2025 @ 1:47p

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: Books
        // Optional search by title and optional genre filter
        public async Task<IActionResult> Index(string? searchString, int? genreId)
        {
            var booksQuery = _context.Books.Include(b => b.Genre).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                booksQuery = booksQuery.Where(b => b.Title.Contains(searchString));
            }

            if (genreId.HasValue && genreId.Value > 0)
            {
                booksQuery = booksQuery.Where(b => b.GenreID == genreId.Value);
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentGenre"] = genreId;
            ViewData["Genres"] = new SelectList(await _context.Genres.OrderBy(g => g.GenreName).ToListAsync(), "GenreID", "GenreName");

            var books = await booksQuery.OrderBy(b => b.Title).ToListAsync();
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.BookID == id.Value);

            if (book == null) return NotFound();

            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            ViewData["GenreID"] = new SelectList(await _context.Genres.OrderBy(g => g.GenreName).ToListAsync(), "GenreID", "GenreName");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenreID,BookNumber,Title,Description,Price,Cost,PublishDate,InventoryQuantity,ReorderPoint,Authors,BookStatus")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreID"] = new SelectList(await _context.Genres.OrderBy(g => g.GenreName).ToListAsync(), "GenreID", "GenreName", book.GenreID);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books.FindAsync(id.Value);
            if (book == null) return NotFound();

            ViewData["GenreID"] = new SelectList(await _context.Genres.OrderBy(g => g.GenreName).ToListAsync(), "GenreID", "GenreName", book.GenreID);
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookID,GenreID,BookNumber,Title,Description,Price,Cost,PublishDate,InventoryQuantity,ReorderPoint,Authors,BookStatus")] Book book)
        {
            if (id != book.BookID) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreID"] = new SelectList(await _context.Genres.OrderBy(g => g.GenreName).ToListAsync(), "GenreID", "GenreName", book.GenreID);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.BookID == id.Value);

            if (book == null) return NotFound();

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }
    }
}