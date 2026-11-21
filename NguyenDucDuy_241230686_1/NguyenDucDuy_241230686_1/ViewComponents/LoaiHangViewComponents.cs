using Microsoft.EntityFrameworkCore;
using NguyenDucDuy_241230686_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace NguyenDucDuy_241230686_1.ViewComponents
{
    public class LoaiHangViewComponent : ViewComponent
    {
        private readonly QLHangHoaContext _context;
        public LoaiHangViewComponent(QLHangHoaContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var dsloaiHang = await _context.LoaiHangs.ToListAsync();
            return View(dsloaiHang  );
        }
    
    }
}
