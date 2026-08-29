using Microsoft.AspNetCore.Mvc;
using MyAppMVC.Models;

namespace MyAppMVC.Controllers
{
    public class ProductController : Controller
    {
        private List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 590000, CreatedAt = new DateTime(2023, 12, 25), Image = "/images/bag1.png" },
                new Product { Id = 2, Name = "Product 2", Price = 780000, CreatedAt = new DateTime(2023, 12, 25), Image = "/images/bag2.png" },
                new Product { Id = 3, Name = "Product 3", Price = 850000, CreatedAt = new DateTime(2023, 12, 25), Image = "/images/bag3.png" },
                new Product { Id = 4, Name = "Product 4", Price = 550000, CreatedAt = new DateTime(2023, 12, 25), Image = "/images/bag4.png" }
            };
        }

        public IActionResult Index()
        {
            var products = GetProducts();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = GetProducts().FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
