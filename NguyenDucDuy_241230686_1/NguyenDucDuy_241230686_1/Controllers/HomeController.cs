using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDucDuy_241230686_1.Models;
using System.Diagnostics;

namespace NguyenDucDuy_241230686_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLHangHoaContext _context;

        public HomeController(QLHangHoaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dsHangHoa = await _context.HangHoas.Where(x => x.Gia >= 100).ToListAsync(); 
            return View(dsHangHoa);
        }


        [HttpGet]
        public IActionResult LocHang(int maLoai)
        {
            var dsHangHoa = _context.HangHoas
                .Where(x => x.MaLoai == maLoai && x.Gia >= 100)
                .ToList();

            return PartialView(
                "~/Views/Home/_DanhSachHangHoa.cshtml",
                dsHangHoa
            );
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
