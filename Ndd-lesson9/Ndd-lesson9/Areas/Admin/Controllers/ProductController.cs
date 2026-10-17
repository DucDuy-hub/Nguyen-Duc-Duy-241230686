using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ndd_lesson9.Data;
using Ndd_lesson9.Models;

namespace Ndd_lesson9.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController(StoreDbContext db) : Controller
{
    public async Task<IActionResult> Index(string? search)
    {
        ViewData["Search"] = search;
        var query = db.Products.AsNoTracking().Include(product => product.Category).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToUpper();
            query = query.Where(product => product.Name.ToUpper().Contains(term));
        }

        return View(await query.OrderBy(product => product.Name).ToListAsync());
    }

    public async Task<IActionResult> Create()
    {
        await LoadCategoriesAsync();
        return View(new Product());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        product.Name = product.Name.Trim();
        if (await NameExistsAsync(product.Name))
        {
            ModelState.AddModelError(nameof(product.Name), "Tên sản phẩm đã tồn tại.");
        }

        if (!await db.Categories.AnyAsync(category => category.Id == product.CategoryId))
        {
            ModelState.AddModelError(nameof(product.CategoryId), "Danh mục không còn tồn tại.");
        }

        if (!ModelState.IsValid)
        {
            await LoadCategoriesAsync(product.CategoryId);
            return View(product);
        }

        db.Products.Add(product);
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã thêm sản phẩm.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var product = await db.Products.AsNoTracking().Include(item => item.Category).FirstOrDefaultAsync(item => item.Id == id);
        return product is null ? NotFound() : View(product);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var product = await db.Products.FindAsync(id);
        if (product is null) return NotFound();
        await LoadCategoriesAsync(product.CategoryId);
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id) return NotFound();
        product.Name = product.Name.Trim();
        if (await NameExistsAsync(product.Name, id))
        {
            ModelState.AddModelError(nameof(product.Name), "Tên sản phẩm đã tồn tại.");
        }

        if (!await db.Categories.AnyAsync(category => category.Id == product.CategoryId))
        {
            ModelState.AddModelError(nameof(product.CategoryId), "Danh mục không còn tồn tại.");
        }

        if (!ModelState.IsValid)
        {
            await LoadCategoriesAsync(product.CategoryId);
            return View(product);
        }

        db.Products.Update(product);
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã cập nhật sản phẩm.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var product = await db.Products.AsNoTracking().Include(item => item.Category).FirstOrDefaultAsync(item => item.Id == id);
        return product is null ? NotFound() : View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await db.Products.FindAsync(id);
        if (product is null) return NotFound();
        db.Products.Remove(product);
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa sản phẩm.";
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadCategoriesAsync(int? selectedId = null)
    {
        ViewData["CategoryId"] = new SelectList(await db.Categories.OrderBy(category => category.Name).ToListAsync(), "Id", "Name", selectedId);
    }

    private Task<bool> NameExistsAsync(string name, int? exceptId = null)
    {
        var normalized = name.ToUpper();
        var excludedId = exceptId ?? 0;
        return db.Products.AnyAsync(item => item.Name.ToUpper() == normalized && item.Id != excludedId);
    }
}
