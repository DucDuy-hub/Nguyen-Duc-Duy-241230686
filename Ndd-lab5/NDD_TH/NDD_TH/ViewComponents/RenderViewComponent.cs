using Microsoft.AspNetCore.Mvc;
using NDD_TH.Models;

namespace NDD_TH.ViewComponents;

public class RenderViewComponent : ViewComponent
{
    private static readonly IReadOnlyList<MenuItem> MenuItems =
    [
        new MenuItem { Id = 1, Name = "Dashboard", Link = "/Home/Index" },
        new MenuItem { Id = 2, Name = "Người học", Link = "/Learner/Index" },
        new MenuItem { Id = 3, Name = "Môn học", Link = "/Course/Index" },
        new MenuItem { Id = 4, Name = "Lab sinh viên", Link = "/Admin/Student/List" }
    ];

    public IViewComponentResult Invoke() => View("RenderLeftMenu", MenuItems);
}
