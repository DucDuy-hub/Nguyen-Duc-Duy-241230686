using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ndd_lesson9.Data;
using Ndd_lesson9.Models;
using System.Diagnostics;

namespace Ndd_lesson9.Controllers
{
    public class HomeController : Controller
    {
        private readonly StoreDbContext _db;

        public HomeController(StoreDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var model = new StoreHomeViewModel
            {
                Categories = await _db.Categories.Where(category => category.Status == 1).OrderBy(category => category.Id).ToListAsync(),
                FeaturedProducts = await _db.Products.Include(product => product.Category)
                    .Where(product => product.Status == 1)
                    .OrderBy(product => product.Id)
                    .Take(3)
                    .ToListAsync(),
                FeaturedBanner = await _db.Banners.Where(banner => banner.Status == 1).OrderBy(banner => banner.Priority).FirstOrDefaultAsync()
            };

            return View(model);
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
