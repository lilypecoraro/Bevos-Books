using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Controllers
{
    [Authorize]
    public class ItemDiscountsController : Controller
    {
        private readonly AppDbContext _context;

        public ItemDiscountsController(AppDbContext context)
        {
            _context = context;
        }

        // Admin list
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("Admin")) return View("AccessDenied");

            var discounts = await _context.ItemDiscounts
                .Include(d => d.Book)
                .OrderBy(d => d.Book.Title)
                .ToListAsync();

            return View(discounts);
        }

        // Create for a specific book
        public async Task<IActionResult> Create(int bookId)
        {
            if (!User.IsInRole("Admin")) return View("AccessDenied");

            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return NotFound();

            ViewBag.Book = book;
            return View(new ItemDiscount { BookID = bookId, Status = "Enabled" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemDiscount discount)
        {
            if (!User.IsInRole("Admin")) return View("AccessDenied");

            // ?? Ensure book exists first
            var book = await _context.Books.FindAsync(discount.BookID);
            if (book == null) return NotFound();

            // ?? Ignore nav/server-side properties that aren't posted by the form
            ModelState.Remove(nameof(ItemDiscount.Book));         // navigation property
                                                                  // If you have others like navigation collections, remove them too:
                                                                  // ModelState.Remove(nameof(ItemDiscount.SomeNavProp));

            // basic validation
            if (discount.DiscountPercent.HasValue && discount.DiscountPercent.Value < 0)
                ModelState.AddModelError(nameof(discount.DiscountPercent), "Percent cannot be negative.");

            if (discount.DiscountAmount.HasValue && discount.DiscountAmount.Value < 0)
                ModelState.AddModelError(nameof(discount.DiscountAmount), "Amount cannot be negative.");

            // (Optional but nice) Require at least one of percent or amount:
            // if (!discount.DiscountPercent.HasValue && !discount.DiscountAmount.HasValue)
            //     ModelState.AddModelError("", "Please enter either a percent or amount discount.");

            if (!ModelState.IsValid)
            {
                ViewBag.Book = book;
                return View(discount);
            }

            _context.ItemDiscounts.Add(discount);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Discount added to '{book.Title}'.";
            return RedirectToAction("Details", "Books", new { id = book.BookID });
        }

        // Toggle enable/disable
        public async Task<IActionResult> Toggle(int id)
        {
            if (!User.IsInRole("Admin")) return View("AccessDenied");

            var disc = await _context.ItemDiscounts.Include(d => d.Book).FirstOrDefaultAsync(d => d.ItemDiscountID == id);
            if (disc == null) return NotFound();

            disc.Status = disc.Status == "Enabled" ? "Disabled" : "Enabled";
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Discount for '{disc.Book.Title}' {(disc.Status == "Enabled" ? "enabled" : "disabled")}.";
            return RedirectToAction(nameof(Index));
        }
    }
}