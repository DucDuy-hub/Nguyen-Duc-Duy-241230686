
using Microsoft.AspNetCore.Mvc;
using Ndd_lab1.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ndd_lab1.Controllers

{

    public class StudentController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public StudentController(IWebHostEnvironment env)
        {
            _env = env;
        }
        private static List<Student> ListStudents = new List<Student>
        {
            new Student { Id = 101, Name = "Hải Nam", DateOfBirth = new DateTime(2002, 5, 12), Score = 8.5, Branch = Branch.IT, Gender = Gender.Male, IsRegular = true, Address = "A1-2018", Email = "nam@gmail.com", Image = "/images/nam.jpg" },
            new Student { Id = 102, Name = "Minh Tú", DateOfBirth = new DateTime(2003, 8, 24), Score = 7.8, Branch = Branch.BE, Gender = Gender.Female, IsRegular = true, Address = "A1-2019", Email = "tu@gmail.com", Image = "/images/tu.jpg" },
            new Student { Id = 103, Name = "Hoàng Phong", DateOfBirth = new DateTime(2001, 11, 5), Score = 9.1, Branch = Branch.CE, Gender = Gender.Male, IsRegular = false, Address = "A1-2020", Email = "phong@gmail.com", Image = "/images/phong.jpg" },
            new Student { Id = 104, Name = "Xuân Mai", DateOfBirth = new DateTime(2004, 2, 18), Score = 8.0, Branch = Branch.EE, Gender = Gender.Female, IsRegular = false, Address = "A1-2021", Email = "mai@gmail.com", Image = "/images/mai.jpg" }
        };

        public IActionResult Index()
        {
            return View(ListStudents);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Student s, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(s);
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                string extension = Path.GetExtension(imageFile.FileName).ToLower();

                if (extension != ".jpg" && extension != ".jpeg" &&
                    extension != ".png" && extension != ".webp")
                {
                    ModelState.AddModelError(nameof(imageFile), "Chỉ chấp nhận ảnh JPG, PNG hoặc WEBP.");
                    return View(s);
                }

                string folder = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid().ToString() + extension;
                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                s.Image = "/images/" + fileName;
            }

            s.Id = ListStudents.Count == 0 ? 101 : ListStudents.Max(x => x.Id) + 1;
            ListStudents.Add(s);

            return RedirectToAction(nameof(Index));
        }

    }
}
