
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
            new Student { Id = 101, Name = "Hải Nam", Branch = Branch.IT, Gender = Gender.Male, IsRegular = true, Address = "A1-2018", Email = "nam@gmail.com" },
            new Student { Id = 102, Name = "Minh Tú", Branch = Branch.BE, Gender = Gender.Female, IsRegular = true, Address = "A1-2019", Email = "tu@gmail.com" },
            new Student { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE, Gender = Gender.Male, IsRegular = false, Address = "A1-2020", Email = "phong@gmail.com" },
            new Student { Id = 104, Name = "Xuân Mai", Branch = Branch.EE, Gender = Gender.Female, IsRegular = false, Address = "A1-2021", Email = "mai@gmail.com" }
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
            s.Id = ListStudents.Max(x => x.Id) + 1;

            if (imageFile != null && imageFile.Length > 0)
            {
                string extension = Path.GetExtension(imageFile.FileName).ToLower();

                if (extension != ".jpg" && extension != ".jpeg" &&
                    extension != ".png" && extension != ".webp")
                {
                    return BadRequest("Chỉ chấp nhận ảnh JPG, PNG hoặc WEBP.");
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

            ListStudents.Add(s);

            return RedirectToAction("Index");
        }

    }
}
