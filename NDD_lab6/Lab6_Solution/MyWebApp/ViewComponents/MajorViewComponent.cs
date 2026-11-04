using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.ViewComponents
{
    public class MajorViewComponent : ViewComponent
    {
        private readonly SchoolContext db;

        // SchoolContext được DI (Dependency Injection) tự động truyền vào
        public MajorViewComponent(SchoolContext _context)
        {
            db = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Lấy danh sách chuyên ngành và trả về view RenderMajor
            List<Major> majors = await db.Majors.OrderBy(m => m.MajorID).ToListAsync();
            return View("RenderMajor", majors);
        }
    }
}
