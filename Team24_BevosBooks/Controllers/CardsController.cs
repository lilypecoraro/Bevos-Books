// THIS IS A TEMPORARY CONTROLLER ADDED ONLY TO SEE THE CARDS DATA BEING SEEDED.

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;

namespace Team24_BevosBooks.Controllers
{
    public class CardsController : Controller
    {
        private readonly AppDbContext _context;

        public CardsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Cards
        public async Task<IActionResult> Index()
        {
            var cards = await _context.Cards
                .Include(c => c.User)
                .ToListAsync();
            return View(cards);
        }
    }
}