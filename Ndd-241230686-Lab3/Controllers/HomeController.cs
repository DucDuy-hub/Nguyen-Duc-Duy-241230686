using Microsoft.AspNetCore.Mvc;
using Ndd_241230686_Lab3.Models;

namespace Ndd_241230686_Lab3.Controllers
{
    public class HomeController : Controller
    {
        private readonly Product product = new Product();
        private readonly Category category = new Category();

        public IActionResult Index()
        {
            ViewBag.Categories = category.GetCategoryList();
            return View(product.GetProductList());
        }

        public IActionResult About()
        {
            ViewData["Title"] = "Giới thiệu";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Title"] = "Liên hệ";
            return View();
        }
    }
}
