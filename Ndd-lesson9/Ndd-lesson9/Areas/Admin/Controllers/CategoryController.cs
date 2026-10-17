using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ndd_lesson9.Data;
using Ndd_lesson9.Models;

namespace Ndd_lesson9.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController(StoreDbContext db) : Controller
{
    public async Task<IActionResult> Index(string? search)
    {
        ViewData["Search"] = search;
        var query = db.Categories.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToUpper();
            query = query.Where(category => category.Name.ToUpper().Contains(term));
        }

        return View(await query.OrderBy(category => category.Name).ToListAsync());
    }

    public IActionResult Create() => View(new Category());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        category.Name = category.Name.Trim();
        if (await NameExistsAsync(category.Name))
        {
            ModelState.AddModelError(nameof(category.Name), "Tên danh mục đã tồn tại.");
        }

        if (!ModelState.IsValid)
        {
            return View(category);
        }

        db.Categories.Add(category);
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã thêm danh mục.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var category = await db.Categories.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
        return category is null ? NotFound() : View(category);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var category = await db.Categories.FindAsync(id);
        return category is null ? NotFound() : View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Category category)
    {
        if (id != category.Id) return NotFound();
        category.Name = category.Name.Trim();
        if (await NameExistsAsync(category.Name, id))
        {
            ModelState.AddModelError(nameof(category.Name), "Tên danh mục đã tồn tại.");
        }

        if (!ModelState.IsValid) return View(category);

        db.Categories.Update(category);
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã cập nhật danh mục.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var category = await db.Categories.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
        return category is null ? NotFound() : View(category);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var category = await db.Categories.FindAsync(id);
        if (category is null) return NotFound();
        if (await db.Products.AnyAsync(product => product.CategoryId == id))
        {
            TempData["Error"] = "Không thể xóa danh mục đang có sản phẩm.";
            return RedirectToAction(nameof(Index));
        }

        db.Categories.Remove(category);
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa danh mục.";
        return RedirectToAction(nameof(Index));
    }

    private Task<bool> NameExistsAsync(string name, int? exceptId = null)
    {
        var normalized = name.ToUpper();
        var excludedId = exceptId ?? 0;
        return db.Categories.AnyAsync(item => item.Name.ToUpper() == normalized && item.Id != excludedId);
    }
}
