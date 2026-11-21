
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenDucDuy_241230686_2.Models;

namespace NguyenDucDuy_241230686_2.Controllers
{
    public class TaiSanController : Controller
    {
        private readonly QLTaiSanContext _context;

        public TaiSanController(QLTaiSanContext context)
        {
            _context = context;
        }

        // GET: TaiSan/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var dsLoai = await _context.LoaiTaiSans.ToListAsync();

            ViewBag.DanhSachLoai = new SelectList(
                dsLoai, "MaLoai", "TenLoai"
            );

            return View();
        }

        // POST: TaiSan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaiSan taiSan)
        {
            if (ModelState.IsValid)
            {
                _context.TaiSans.Add(taiSan);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            var dsLoai = await _context.LoaiTaiSans.ToListAsync();

            ViewBag.DanhSachLoai = new SelectList(
                dsLoai, "MaLoai", "TenLoai", taiSan.MaLoai
            );

            return View(taiSan);
        }
    }
}
