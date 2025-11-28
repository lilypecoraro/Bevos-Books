using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Controllers
{
    public class GenresController : Controller
    {
        private readonly AppDbContext _context;

        public GenresController(AppDbContext context)
        {
            _context = context;
        }

        // ============================================
        // INDEX
        // ============================================
        public async Task<IActionResult> Index()
        {
            var genres = await _context.Genres
                .OrderBy(g => g.GenreName)
                .ToListAsync();

            return View(genres);
        }

        // ============================================
        // CREATE - GET
        // ============================================
        public IActionResult Create()
        {
            return View();
        }

        // ============================================
        // CREATE - POST
        // ============================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenreID,GenreName")] Genre genre)
        {
            if (!string.IsNullOrWhiteSpace(genre.GenreName))
            {
                genre.GenreName = genre.GenreName.Trim();
            }

            if (string.IsNullOrWhiteSpace(genre.GenreName))
            {
                ModelState.AddModelError("", "Genre name cannot be empty.");
            }

            bool exists = await _context.Genres
                .AnyAsync(g => g.GenreName.ToLower() == genre.GenreName.ToLower());

            if (exists)
            {
                ModelState.AddModelError("", "That genre already exists.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(genre);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"Genre '{genre.GenreName}' was successfully created.";

                return RedirectToAction(nameof(Index));
            }

            return View(genre);
        }

        // ============================================
        // EDIT - GET
        // ============================================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
                return NotFound();

            return View(genre);
        }

        // ============================================
        // EDIT - POST
        // ============================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Genre genre)
        {
            if (id != genre.GenreID)
                return NotFound();

            if (!string.IsNullOrWhiteSpace(genre.GenreName))
            {
                genre.GenreName = genre.GenreName.Trim();
            }

            if (string.IsNullOrWhiteSpace(genre.GenreName))
            {
                ModelState.AddModelError("", "Genre name cannot be empty.");
            }

            var original = await _context.Genres
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.GenreID == id);

            if (original == null)
                return NotFound();

            if (!string.Equals(original.GenreName, genre.GenreName, StringComparison.OrdinalIgnoreCase))
            {
                bool duplicateExists = await _context.Genres
                    .AnyAsync(g => g.GenreName.ToLower() == genre.GenreName.ToLower()
                                && g.GenreID != genre.GenreID);

                if (duplicateExists)
                {
                    ModelState.AddModelError("", "A genre with that name already exists.");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genre);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = $"Genre '{genre.GenreName}' was successfully updated.";

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "An error occurred while saving this genre.");
                }
            }

            return View(genre);
        }
    }
}