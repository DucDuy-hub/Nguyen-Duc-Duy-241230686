using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ndd_lesson9.Data;
using Ndd_lesson9.Models;

namespace Ndd_lesson9.Areas.Admin.Controllers;

[Area("Admin")]
public class BannerController(StoreDbContext db) : Controller
{
    public async Task<IActionResult> Index(string? search)
    {
        ViewData["Search"] = search;
        var query = db.Banners.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToUpper();
            query = query.Where(banner => banner.Name.ToUpper().Contains(term));
        }

        return View(await query.OrderBy(banner => banner.Priority).ThenBy(banner => banner.Name).ToListAsync());
    }

    public IActionResult Create() => View(new Banner());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Banner banner)
    {
        banner.Name = banner.Name.Trim();
        if (await NameExistsAsync(banner.Name)) ModelState.AddModelError(nameof(banner.Name), "Tên banner đã tồn tại.");
        if (!ModelState.IsValid) return View(banner);
        db.Banners.Add(banner);
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã thêm banner.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var banner = await db.Banners.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
        return banner is null ? NotFound() : View(banner);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var banner = await db.Banners.FindAsync(id);
        return banner is null ? NotFound() : View(banner);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Banner banner)
    {
        if (id != banner.Id) return NotFound();
        banner.Name = banner.Name.Trim();
        if (await NameExistsAsync(banner.Name, id)) ModelState.AddModelError(nameof(banner.Name), "Tên banner đã tồn tại.");
        if (!ModelState.IsValid) return View(banner);
        db.Banners.Update(banner);
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã cập nhật banner.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var banner = await db.Banners.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
        return banner is null ? NotFound() : View(banner);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var banner = await db.Banners.FindAsync(id);
        if (banner is null) return NotFound();
        db.Banners.Remove(banner);
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa banner.";
        return RedirectToAction(nameof(Index));
    }

    private Task<bool> NameExistsAsync(string name, int? exceptId = null)
    {
        var normalized = name.ToUpper();
        var excludedId = exceptId ?? 0;
        return db.Banners.AnyAsync(item => item.Name.ToUpper() == normalized && item.Id != excludedId);
    }
}
