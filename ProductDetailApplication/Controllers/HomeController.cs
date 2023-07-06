using Microsoft.AspNetCore.Mvc;

namespace ProductDetailApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
