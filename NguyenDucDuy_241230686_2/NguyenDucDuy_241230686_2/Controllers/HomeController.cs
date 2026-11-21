using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDucDuy_241230686_2.Models;
using System.Diagnostics;

namespace NguyenDucDuy_241230686_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLTaiSanContext _context;

        public HomeController(QLTaiSanContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var dsLoai = await _context.LoaiTaiSans.ToListAsync();
            ViewBag.DanhSachLoai = dsLoai;

            var dsTaiSan = await _context.TaiSans.Where(x => x.NguyenGia >= 20).ToListAsync();
            return View(dsTaiSan);
        }


        [HttpGet]
        public async Task<IActionResult> LocTaiSan(int maLoai)
        {
            var dsTaiSan = await _context.TaiSans
                .Where(x => x.MaLoai == maLoai)
                .ToListAsync();

            return PartialView("_DanhSachTaiSan", dsTaiSan);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
