using Microsoft.AspNetCore.Mvc;
using NDD_TH.Models;
using NDD_TH.Services;

namespace NDD_TH.Controllers;

public class StudentController(StudentStore store, IWebHostEnvironment environment) : Controller
{
    private const long MaxAvatarSize = 5 * 1024 * 1024;
    private static readonly HashSet<string> AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".webp"];

    public IActionResult Index()
    {
        ViewData["Title"] = "Danh sách sinh viên";
        return View(store.GetAll());
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = "Thêm sinh viên";
        return View(new Student());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student, IFormFile? avatar)
    {
        if (avatar is not null)
        {
            var extension = Path.GetExtension(avatar.FileName).ToLowerInvariant();
            if (avatar.Length > MaxAvatarSize)
            {
                ModelState.AddModelError(nameof(avatar), "Ảnh đại diện không được vượt quá 5 MB.");
            }
            else if (!AllowedImageExtensions.Contains(extension))
            {
                ModelState.AddModelError(nameof(avatar), "Chỉ chấp nhận ảnh JPG, PNG hoặc WebP.");
            }
        }

        if (!ModelState.IsValid)
        {
            return View(student);
        }

        if (avatar is { Length: > 0 })
        {
            var uploadDirectory = Path.Combine(environment.WebRootPath, "uploads", "students");
            Directory.CreateDirectory(uploadDirectory);

            var fileName = $"{Guid.NewGuid():N}{Path.GetExtension(avatar.FileName).ToLowerInvariant()}";
            var filePath = Path.Combine(uploadDirectory, fileName);
            await using var stream = System.IO.File.Create(filePath);
            await avatar.CopyToAsync(stream);
            student.AvatarUrl = $"/uploads/students/{fileName}";
        }

        store.Add(student);
        TempData["Success"] = "Đã thêm sinh viên mới.";
        return RedirectToAction(nameof(Index));
    }
}
