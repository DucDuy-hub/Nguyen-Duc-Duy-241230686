using Microsoft.AspNetCore.Mvc;
using Ndd_lab2.Models;
using System.Diagnostics;

namespace Ndd_lab2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Section(string name)
        {
            var availableSections = new[] { "Branches", "Subjects", "Courses" };
            if (!availableSections.Contains(name, StringComparer.OrdinalIgnoreCase))
            {
                return NotFound();
            }

            ViewData["Title"] = name;
            return View(model: name);
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
