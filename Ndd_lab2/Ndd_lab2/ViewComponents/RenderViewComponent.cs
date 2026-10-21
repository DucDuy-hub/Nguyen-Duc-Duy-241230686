using Microsoft.AspNetCore.Mvc;
using Ndd_lab2.Models;

namespace Ndd_lab2.ViewComponents;

public class RenderViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var menuItems = new List<MenuItem>
        {
            new()
            {
                Title = "Dashboard",
                Icon = "fas fa-gauge-high",
                Url = "/Home/Index",
                Section = "Core"
            },
            new()
            {
                Title = "Branches",
                Icon = "fas fa-chart-bar",
                Url = "/Home/Section?name=Branches",
                Section = "Addons"
            },
            new()
            {
                Title = "Students",
                Icon = "fas fa-chart-bar",
                Url = "/Student/Index",
                Section = "Addons"
            },
            new()
            {
                Title = "Subjects",
                Icon = "fas fa-chart-bar",
                Url = "/Home/Section?name=Subjects",
                Section = "Addons"
            },
            new()
            {
                Title = "Courses",
                Icon = "fas fa-chart-bar",
                Url = "/Home/Section?name=Courses",
                Section = "Addons"
            }
        };

        return View(menuItems);
    }
}
