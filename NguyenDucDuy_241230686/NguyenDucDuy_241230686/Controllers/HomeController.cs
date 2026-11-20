
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDucDuy_241230686.Models;
using System.Diagnostics;

namespace NguyenDucDuy_241230686.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLNhaSachContext _context;

        public HomeController(QLNhaSachContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dsSach = await _context.Saches
                .Where(s => s.Gia >= 100)
                .ToListAsync();

            return View(dsSach);
        }

        public async Task<IActionResult> LocSach(int maTL)
        {
            var dsSach = await _context.Saches
                .Where(s => s.MaTl == maTL && s.Gia >= 100)
                .ToListAsync();

            return PartialView("_DanhSachSach", dsSach);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}
