using Microsoft.AspNetCore.Mvc;

namespace Ndd_lesson9.Areas.Admin.Components;

public class NavLeftViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        ViewData["ActiveController"] = ViewContext.RouteData.Values["controller"]?.ToString();
        return View();
    }
}
