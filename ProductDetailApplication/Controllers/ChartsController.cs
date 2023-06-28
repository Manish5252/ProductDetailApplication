using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductDetailApplication.Models;

namespace ProductDetailApplication.Controllers
{
    public class ChartsController : Controller
    {
        private readonly ProductDBContext _context;
        public ChartsController(ProductDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetProductDetails()
        {
            return Json(_context.ProductDetails.ToListAsync());
          
        }
    }
}
