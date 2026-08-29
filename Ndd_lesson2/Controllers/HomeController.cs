using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyAppMVC.Models;

namespace MyAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product Name 1", Price = 590000, CreatedAt = new DateTime(2023, 12, 25), Image = "/images/bag1.png" },
                new Product { Id = 2, Name = "Product Name 2", Price = 780000, CreatedAt = new DateTime(2023, 12, 25), Image = "/images/bag2.png" },
                new Product { Id = 3, Name = "Product Name 3", Price = 850000, CreatedAt = new DateTime(2023, 12, 25), Image = "/images/bag3.png" },
                new Product { Id = 4, Name = "Product Name 4", Price = 550000, CreatedAt = new DateTime(2023, 12, 25), Image = "/images/bag4.png" }
            };

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
