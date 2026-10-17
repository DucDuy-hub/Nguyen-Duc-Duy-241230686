using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ndd_lesson9.Data;
using Ndd_lesson9.Models;

namespace Ndd_lesson9.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController(StoreDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var model = new AdminDashboardViewModel
        {
            CategoryCount = await db.Categories.CountAsync(),
            ProductCount = await db.Products.CountAsync(),
            BannerCount = await db.Banners.CountAsync(),
            BlogCount = await db.Blogs.CountAsync(),
            RecentProducts = await db.Products.Include(product => product.Category)
                .OrderByDescending(product => product.CreatedDate)
                .Take(5)
                .ToListAsync()
        };

        return View(model);
    }
}
