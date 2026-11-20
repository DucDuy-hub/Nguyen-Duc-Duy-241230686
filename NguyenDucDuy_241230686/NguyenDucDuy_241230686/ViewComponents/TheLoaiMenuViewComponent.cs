
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDucDuy_241230686.Models;

namespace NguyenDucDuy_241230686.ViewComponents
{
    public class TheLoaiMenuViewComponent : ViewComponent
    {
        private readonly QLNhaSachContext _context;

        public TheLoaiMenuViewComponent(QLNhaSachContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ds = await _context.TheLoais
                .AsNoTracking()
                .ToListAsync();

            return View(ds);
        }
    }
}
