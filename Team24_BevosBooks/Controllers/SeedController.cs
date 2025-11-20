using Microsoft.AspNetCore.Mvc;

using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Seeding;

namespace Team24_BevosBooks.Controllers
{
    public class SeedController : Controller
    {
        private readonly AppDbContext _context;

        public SeedController(AppDbContext db)
        {
            _context = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SeedBooks()
        {
            try
            {
                SeedBooks.SeedAllBooks(_context);
            }
            catch (Exception ex)
            {
                return View("Error", GetErrorList(ex));
            }

            return View("Confirm");
        }

        public IActionResult SeedCustomers()
        {
            try
            {
                SeedCustomers.SeedAllCustomers(_context);
            }
            catch (Exception ex)
            {
                return View("Error", GetErrorList(ex));
            }

            return View("Confirm");
        }

        public IActionResult SeedEmployees()
        {
            try
            {
                SeedEmployees.SeedAllEmployees(_context);
            }
            catch (Exception ex)
            {
                return View("Error", GetErrorList(ex));
            }

            return View("Confirm");
        }

        public IActionResult SeedReviews()
        {
            try
            {
                SeedReviews.SeedAllReviews(_context);
            }
            catch (Exception ex)
            {
                return View("Error", GetErrorList(ex));
            }

            return View("Confirm");
        }

        public IActionResult SeedCards()
        {
            try
            {
                SeedCards.SeedAllCards(_context);
            }
            catch (Exception ex)
            {
                return View("Error", GetErrorList(ex));
            }

            return View("Confirm");
        }

        public IActionResult SeedOrders()
        {
            try
            {
                SeedOrders.SeedAllOrders(_context);
            }
            catch (Exception ex)
            {
                return View("Error", GetErrorList(ex));
            }

            return View("Confirm");
        }

        private List<String> GetErrorList(Exception ex)
        {
            List<String> errors = new List<String>();
            errors.Add(ex.Message);

            if (ex.InnerException != null)
            {
                errors.Add(ex.InnerException.Message);

                if (ex.InnerException.InnerException != null)
                {
                    errors.Add(ex.InnerException.InnerException.Message);
                }
            }

            return errors;
        }
    }
}
