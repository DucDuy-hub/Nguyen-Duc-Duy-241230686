using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NDD_TH.Data;
using NDD_TH.Models;

namespace NDD_TH.Controllers;

public class LearnerController(SchoolContext context) : Controller
{
    public IActionResult Index(int? mid)
    {
        ViewData["Title"] = "Danh sách người học";
        ViewBag.SelectedMajorId = mid;

        var learners = context.Learners
            .AsNoTracking()
            .Include(learner => learner.Major)
            .OrderBy(learner => learner.LearnerId);

        if (mid.HasValue)
        {
            learners = learners.Where(learner => learner.MajorId == mid.Value)
                .OrderBy(learner => learner.LearnerId);
        }

        return View(learners.ToList());
    }

    [HttpGet]
    public IActionResult LearnerByMajorID(int? mid)
    {
        var learners = context.Learners
            .AsNoTracking()
            .Include(learner => learner.Major)
            .OrderBy(learner => learner.LearnerId);

        if (mid.HasValue)
        {
            learners = learners.Where(learner => learner.MajorId == mid.Value)
                .OrderBy(learner => learner.LearnerId);
        }

        return PartialView("LearnerTable", learners.ToList());
    }

    [HttpGet]
    public IActionResult Create()
    {
        PopulateMajors();
        return View(new Learner { EnrollmentDate = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("FirstMidName,LastName,EnrollmentDate,MajorId")] Learner learner)
    {
        if (ModelState.IsValid)
        {
            context.Learners.Add(learner);
            context.SaveChanges();
            TempData["Success"] = "Đã thêm người học mới.";
            return RedirectToAction(nameof(Index));
        }

        PopulateMajors(learner.MajorId);
        return View(learner);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id is null)
        {
            return BadRequest();
        }

        var learner = context.Learners.Find(id);
        if (learner is null)
        {
            return NotFound();
        }

        PopulateMajors(learner.MajorId);
        return View(learner);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("LearnerId,FirstMidName,LastName,EnrollmentDate,MajorId")] Learner learner)
    {
        if (id != learner.LearnerId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(learner);
                context.SaveChanges();
                TempData["Success"] = "Đã cập nhật thông tin người học.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LearnerExists(learner.LearnerId))
                {
                    return NotFound();
                }

                throw;
            }
        }

        PopulateMajors(learner.MajorId);
        return View(learner);
    }

    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id is null)
        {
            return BadRequest();
        }

        var learner = context.Learners
            .AsNoTracking()
            .Include(item => item.Major)
            .Include(item => item.Enrollments)
            .FirstOrDefault(item => item.LearnerId == id.Value);

        if (learner is null)
        {
            return NotFound();
        }

        ViewBag.CanDelete = learner.Enrollments.Count == 0;
        return View(learner);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var learner = context.Learners
            .Include(item => item.Enrollments)
            .FirstOrDefault(item => item.LearnerId == id);

        if (learner is null)
        {
            return NotFound();
        }

        if (learner.Enrollments.Count > 0)
        {
            TempData["Error"] = "Không thể xóa người học đang có đăng ký môn học.";
            return RedirectToAction(nameof(Index));
        }

        context.Learners.Remove(learner);
        context.SaveChanges();
        TempData["Success"] = "Đã xóa người học.";
        return RedirectToAction(nameof(Index));
    }

    private void PopulateMajors(int? selectedMajorId = null)
    {
        ViewBag.MajorId = new SelectList(
            context.Majors.AsNoTracking().OrderBy(major => major.MajorName),
            nameof(Major.MajorId),
            nameof(Major.MajorName),
            selectedMajorId);
    }

    private bool LearnerExists(int id) => context.Learners.Any(learner => learner.LearnerId == id);
}
