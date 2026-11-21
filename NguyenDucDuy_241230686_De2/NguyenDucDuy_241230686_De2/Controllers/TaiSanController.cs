
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenDucDuy_241230686_De2.Models;

namespace NguyenDucDuy_241230686_De2.Controllers
{
    public class TaiSanController : Controller
    {
        private readonly QLTaiSanContext _context;

        public TaiSanController(QLTaiSanContext context)
        {
            _context = context;
        }

        // GET: TaiSan/Edit/1
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Tìm tài sản theo mã
            var taiSan = await _context.TaiSans
                .FirstOrDefaultAsync(x => x.MaTs == id);

            if (taiSan == null)
            {
                return NotFound();
            }

            // Lấy danh sách loại tài sản
            var dsLoai = await _context.LoaiTaiSans.ToListAsync();

            ViewBag.DanhSachLoai = new SelectList(
                dsLoai,
                "MaLoai",
                "TenLoai",
                taiSan.MaLoai
            );

            // Truyền tài sản sang form Edit
            return View(taiSan);
        }

        // POST: TaiSan/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaiSan taiSan)
        {
            if (id != taiSan.MaTs)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Tìm bản ghi cần cập nhật
                var taiSanCu = await _context.TaiSans
                    .FirstOrDefaultAsync(x => x.MaTs == id);

                if (taiSanCu == null)
                {
                    return NotFound();
                }

                // Cập nhật thông tin
                taiSanCu.MaLoai = taiSan.MaLoai;
                taiSanCu.TenTs = taiSan.TenTs;
                taiSanCu.NguyenGia = taiSan.NguyenGia;
                taiSanCu.Anh = taiSan.Anh;

                await _context.SaveChangesAsync();

                // Quay về trang chủ
                return RedirectToAction("Index", "Home");
            }

            // Nếu dữ liệu sai, hiển thị lại form
            var dsLoai = await _context.LoaiTaiSans.ToListAsync();

            ViewBag.DanhSachLoai = new SelectList(
                dsLoai,
                "MaLoai",
                "TenLoai",
                taiSan.MaLoai
            );

            return View(taiSan);
        }
    }
}
