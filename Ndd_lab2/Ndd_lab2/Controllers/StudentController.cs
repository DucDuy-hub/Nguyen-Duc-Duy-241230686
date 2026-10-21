using Microsoft.AspNetCore.Mvc;
using Ndd_lab2.Models;

namespace Ndd_lab2.Controllers;

public class StudentController : Controller
{
    private static readonly List<Student> Students =
    [
        new()
        {
            Id = 101, FullName = "Hải Nam", BirthDate = new DateTime(2001, 1, 1),
            Major = "CNTT", Gender = "Nam", ProgramType = "Chính quy",
            ClassName = "A1-2018", Address = "", Email = "nam@g.com"
        },
        new()
        {
            Id = 102, FullName = "Minh Tú", BirthDate = new DateTime(2001, 3, 12),
            Major = "Kinh tế", Gender = "Nữ", ProgramType = "Chính quy",
            ClassName = "A1-2019", Address = "", Email = "tu@g.com"
        },
        new()
        {
            Id = 103, FullName = "Hoàng Phong", BirthDate = new DateTime(2000, 7, 24),
            Major = "Công trình", Gender = "Nam", ProgramType = "Phi chính quy",
            ClassName = "A1-2020", Address = "", Email = "phong@g.com"
        },
        new()
        {
            Id = 104, FullName = "Xuân Mai", BirthDate = new DateTime(2001, 11, 5),
            Major = "Điện - Điện tử", Gender = "Nữ", ProgramType = "Phi chính quy",
            ClassName = "A1-2021", Address = "", Email = "mai@g.com"
        }
    ];

    public IActionResult Index() => View(Students);
}
