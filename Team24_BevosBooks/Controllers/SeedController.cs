using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using Team24_BevosBooks.Seeding;

namespace Team24_BevosBooks.Controllers
{
    public class SeedController : Controller
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public SeedController(AppDbContext context,
                              RoleManager<IdentityRole> roleManager,
                              UserManager<AppUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // -------------------------
        // INDEX PAGE
        // -------------------------
        public IActionResult Index()
        {
            return View();
        }

        // -------------------------
        // SEED ROLES
        // -------------------------
        public async Task<IActionResult> SeedRoles()
        {
            try
            {
                await Team24_BevosBooks.Seeding.SeedRoles.AddAllRoles(_roleManager);
            }
            catch (Exception ex)
            {
                return View("Error", GetErrorList(ex));
            }

            return View("Confirm");
        }

        // -------------------------
        // SEED USERS
        // -------------------------
        public async Task<IActionResult> SeedUsers()
        {
            try
            {
                await Team24_BevosBooks.Seeding.SeedUsers.SeedAllUsers(_userManager);
            }
            catch (Exception ex)
            {
                return View("Error", GetErrorList(ex));
            }

            return View("Confirm");
        }

        // -------------------------
        // SEED GENRES
        // -------------------------
        public IActionResult SeedGenres()
        {
            try
            {
                Team24_BevosBooks.Seeding.SeedGenres.SeedAllGenres(_context);
            }
            catch (Exception ex)
            {
                return View("Error", GetErrorList(ex));
            }

            return View("Confirm");
        }


        // -------------------------
        // SEED BOOKS
        // -------------------------
        public IActionResult SeedBooks()
        {
            try
            {
                Team24_BevosBooks.Seeding.SeedBooks.SeedAllBooks(_context);
            }
            catch (Exception ex)
            {
                return View("Error", GetErrorList(ex));
            }

            return View("Confirm");
        }

        // -------------------------
        // SEED CARDS
        // -------------------------
        public IActionResult SeedCards()
        {
            try
            {
                Team24_BevosBooks.Seeding.SeedCards.SeedAllCards(_context);
            }
            catch (Exception ex)
            {
                return View("Error", GetErrorList(ex));
            }

            return View("Confirm");
        }

        // -------------------------
        // SEED REVIEWS
        // -------------------------
        public IActionResult SeedReviews()
        {
            try
            {
                Team24_BevosBooks.Seeding.SeedReviews.SeedAllReviews(_context);
            }
            catch (Exception ex)
            {
                return View("Error", GetErrorList(ex));
            }

            return View("Confirm");
        }

        // -------------------------
        // SEED ORDERS
        // -------------------------
        public IActionResult SeedOrders()
        {
            try
            {
                Team24_BevosBooks.Seeding.SeedOrders.SeedAllOrders(_context);
            }
            catch (Exception ex)
            {
                return View("Error", GetErrorList(ex));
            }

            return View("Confirm");
        }


        // -------------------------
        // ERROR HELPER
        // -------------------------
        private List<string> GetErrorList(Exception ex)
        {
            List<string> messages = new List<string>();

            messages.Add(ex.Message);

            if (ex.InnerException != null)
            {
                messages.Add(ex.InnerException.Message);

                if (ex.InnerException.InnerException != null)
                {
                    messages.Add(ex.InnerException.InnerException.Message);
                }
            }

            return messages;
        }
    }
}
