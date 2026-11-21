
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenDucDuy_241230686_1.Models;

namespace NguyenDucDuy_241230686_1.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly QLHangHoaContext _context;

        public HangHoaController(QLHangHoaContext context)
        {
            _context = context;
        }

        // Hiển thị form thêm hàng hóa
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadLoaiHang();
            return View(new HangHoaCreateViewModel());
        }

        // Xử lý dữ liệu khi nhấn Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            HangHoaCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool loaiHopLe = await _context.LoaiHangs
                    .AnyAsync(x => x.MaLoai == model.MaLoai);

                if (!loaiHopLe)
                {
                    ModelState.AddModelError(
                        "MaLoai", "Loại hàng không tồn tại");
                }
            }

            if (!ModelState.IsValid)
            {
                await LoadLoaiHang();
                return View(model);
            }

            var hangHoa = new HangHoa
            {
                MaLoai = model.MaLoai!.Value,
                TenHang = model.TenHang,
                Gia = model.Gia,
                Anh = model.Anh
            };

            _context.HangHoas.Add(hangHoa);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        // Load danh sách loại hàng cho dropdown
        private async Task LoadLoaiHang()
        {
            var dsLoai = await _context.LoaiHangs.ToListAsync();

            ViewBag.DanhSachLoai = new SelectList(
                dsLoai, "MaLoai", "TenLoai");
        }
    }
}
