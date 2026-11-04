using Microsoft.AspNetCore.Mvc;
using NDD_TH.Data;
using NDD_TH.Models;
using NDD_TH.Services;
using System.Diagnostics;

namespace NDD_TH.Controllers
{
    public class HomeController(SchoolContext context, StudentStore studentStore) : Controller
    {
        public IActionResult Index()
        {
            ViewBag.LearnerCount = context.Learners.Count();
            ViewBag.MajorCount = context.Majors.Count();
            ViewBag.CourseCount = context.Courses.Count();
            ViewBag.StudentCount = studentStore.GetAll().Count;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
