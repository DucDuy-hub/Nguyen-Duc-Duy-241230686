using Microsoft.AspNetCore.Mvc;
using Ndd_lab1.Models;

namespace Ndd_lab1.ViewComponents
{
    public class RenderViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync()
        {
            var menuItems = new List<MenuItem>
            {
                new() { Id = 1, Name = "Branches", Link = "#branches" },
                new() { Id = 2, Name = "Students", Link = "/Admin/Student/List" },
                new() { Id = 3, Name = "Subjects", Link = "#subjects" },
                new() { Id = 4, Name = "Courses", Link = "#courses" }
            };

            return Task.FromResult<IViewComponentResult>(View("RenderLeftMenu", menuItems));
        }
    }
}
