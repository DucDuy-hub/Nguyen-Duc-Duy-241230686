using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NDD_TH.Data;

namespace NDD_TH.Controllers;

public class CourseController(SchoolContext context) : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Danh sách môn học";
        var courses = context.Courses
            .AsNoTracking()
            .Include(course => course.Enrollments)
            .OrderBy(course => course.CourseId)
            .ToList();

        return View(courses);
    }
}
