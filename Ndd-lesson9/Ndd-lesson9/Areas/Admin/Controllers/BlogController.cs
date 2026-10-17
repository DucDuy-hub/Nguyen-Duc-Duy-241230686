using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ndd_lesson9.Data;
using Ndd_lesson9.Models;

namespace Ndd_lesson9.Areas.Admin.Controllers;

[Area("Admin")]
public class BlogController(StoreDbContext db) : Controller
{
    public async Task<IActionResult> Index(string? search)
    {
        ViewData["Search"] = search;
        var query = db.Blogs.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToUpper();
            query = query.Where(blog => blog.Name.ToUpper().Contains(term));
        }

        return View(await query.OrderByDescending(blog => blog.CreatedDate).ToListAsync());
    }

    public IActionResult Create() => View(new Blog());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Blog blog)
    {
        blog.Name = blog.Name.Trim();
        if (await NameExistsAsync(blog.Name)) ModelState.AddModelError(nameof(blog.Name), "Tiêu đề bài viết đã tồn tại.");
        if (!ModelState.IsValid) return View(blog);
        db.Blogs.Add(blog);
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã thêm bài viết.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var blog = await db.Blogs.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
        return blog is null ? NotFound() : View(blog);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var blog = await db.Blogs.FindAsync(id);
        return blog is null ? NotFound() : View(blog);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Blog blog)
    {
        if (id != blog.Id) return NotFound();
        blog.Name = blog.Name.Trim();
        if (await NameExistsAsync(blog.Name, id)) ModelState.AddModelError(nameof(blog.Name), "Tiêu đề bài viết đã tồn tại.");
        if (!ModelState.IsValid) return View(blog);
        db.Blogs.Update(blog);
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã cập nhật bài viết.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var blog = await db.Blogs.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
        return blog is null ? NotFound() : View(blog);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var blog = await db.Blogs.FindAsync(id);
        if (blog is null) return NotFound();
        db.Blogs.Remove(blog);
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa bài viết.";
        return RedirectToAction(nameof(Index));
    }

    private Task<bool> NameExistsAsync(string name, int? exceptId = null)
    {
        var normalized = name.ToUpper();
        var excludedId = exceptId ?? 0;
        return db.Blogs.AnyAsync(item => item.Name.ToUpper() == normalized && item.Id != excludedId);
    }
}
