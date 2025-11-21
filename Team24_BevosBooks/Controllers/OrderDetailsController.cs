using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly AppDbContext _context;

        public OrderDetailsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var orderDetails = await _context.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Book)
                .Include(od => od.Card)
                .Include(od => od.Coupon)
                .ToListAsync();

            return View(orderDetails);
        }
    }
}