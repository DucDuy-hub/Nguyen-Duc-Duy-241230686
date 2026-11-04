using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NDD_TH.Data;

namespace NDD_TH.ViewComponents;

public class MajorViewComponent(SchoolContext context) : ViewComponent
{
    public IViewComponentResult Invoke(int? selectedMajorId)
    {
        ViewBag.SelectedMajorId = selectedMajorId;
        var majors = context.Majors
            .AsNoTracking()
            .OrderBy(major => major.MajorName)
            .ToList();

        return View(majors);
    }
}
