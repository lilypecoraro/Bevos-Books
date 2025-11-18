using Microsoft.AspNetCore.Mvc;

namespace Team24_BevosBooks.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
