using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;

namespace Team24_BevosBooks.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToListAsync();
            return View(users);
        }
    }
}