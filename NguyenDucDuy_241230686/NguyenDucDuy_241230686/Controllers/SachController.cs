
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenDucDuy_241230686.Models;

namespace NguyenDucDuy_241230686.Controllers
{
    public class SachController : Controller
    {
        private readonly QLNhaSachContext _context;
        private readonly IWebHostEnvironment _environment;

        public SachController(
            QLNhaSachContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private async Task NapTheLoai()
        {
            var dsTheLoai = await _context.TheLoais
                .AsNoTracking()
                .ToListAsync();

            ViewBag.DanhSachTheLoai = new SelectList(
                dsTheLoai, "MaTl", "TenTl");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await NapTheLoai();
            return View(new CreateSachViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            CreateSachViewModel model)
        {
            // Kiểm tra thể loại có tồn tại hay không
            if (model.MaTl.HasValue)
            {
                bool tonTai = await _context.TheLoais
                    .AnyAsync(t => t.MaTl == model.MaTl.Value);

                if (!tonTai)
                {
                    ModelState.AddModelError(
                        "MaTl", "Thể loại không hợp lệ");
                }
            }

            // Kiểm tra định dạng ảnh
            if (model.Anh != null)
            {
                string duoiAnh = Path.GetExtension(
                    model.Anh.FileName).ToLowerInvariant();

                string[] duoiHopLe =
                {
                    ".jpg", ".jpeg", ".png", ".webp"
                };

                if (!duoiHopLe.Contains(duoiAnh))
                {
                    ModelState.AddModelError(
                        "Anh",
                        "Ảnh phải có đuôi .jpg, .jpeg, .png hoặc .webp");
                }
            }

            if (!ModelState.IsValid)
            {
                await NapTheLoai();
                return View(model);
            }

            string? tenAnh = null;

            if (model.Anh != null)
            {
                string thuMuc = Path.Combine(
                    _environment.WebRootPath, "images");

                Directory.CreateDirectory(thuMuc);

                string duoiAnh = Path.GetExtension(
                    model.Anh.FileName).ToLowerInvariant();

                tenAnh = Guid.NewGuid().ToString("N") + duoiAnh;

                string duongDan = Path.Combine(thuMuc, tenAnh);

                using (var stream = new FileStream(
                    duongDan, FileMode.CreateNew))
                {
                    await model.Anh.CopyToAsync(stream);
                }
            }

            var sach = new Sach
            {
                TenSach = model.TenSach.Trim(),
                MaTl = model.MaTl!.Value,
                Gia = model.Gia,
                Anh = tenAnh
            };

            _context.Saches.Add(sach);
            await _context.SaveChangesAsync();

            TempData["ThongBao"] = "Thêm sách thành công!";
            return RedirectToAction("Index", "Home");
        }
    }
}
