using DBFirstApp.Data;
using DBFirstApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DBFirstApp.Controllers;

public class LearnerController(SchoolContext context) : Controller
{
    public IActionResult Index()
    {
        var learners = context.Learners
            .AsNoTracking()
            .Include(learner => learner.Major)
            .OrderBy(learner => learner.LearnerId)
            .ToList();

        return View(learners);
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

        var learner = context.Learners.Find(id.Value);
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
            context.Update(learner);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
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
        var learner = context.Learners.Include(item => item.Enrollments)
            .FirstOrDefault(item => item.LearnerId == id);

        if (learner is null)
        {
            return NotFound();
        }

        if (learner.Enrollments.Count > 0)
        {
            TempData["Error"] = "This learner has enrollments and cannot be deleted.";
            return RedirectToAction(nameof(Index));
        }

        context.Learners.Remove(learner);
        context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private void PopulateMajors(int? selectedMajorId = null)
    {
        ViewBag.MajorId = new SelectList(context.Majors.AsNoTracking().OrderBy(major => major.MajorName),
            nameof(Major.MajorId), nameof(Major.MajorName), selectedMajorId);
    }
}
