using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ndd_lab1.Data;
using Ndd_lab1.Models;

namespace Ndd_lab1.Controllers
{
    public class LearnerController(SchoolContext context) : Controller
    {
        private readonly SchoolContext _context = context;

        public async Task<IActionResult> Index()
        {
            var learners = await _context.Learners
                .AsNoTracking()
                .Include(learner => learner.Major)
                .Include(learner => learner.Enrollments)
                .OrderBy(learner => learner.LearnerID)
                .ToListAsync();

            return View(learners);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateMajors();
            return View(new Learner { EnrollmentDate = DateTime.Today });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (!ModelState.IsValid)
            {
                await PopulateMajors(learner.MajorID);
                return View(learner);
            }

            _context.Learners.Add(learner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var learner = await _context.Learners.FindAsync(id.Value);
            if (learner is null)
            {
                return NotFound();
            }

            await PopulateMajors(learner.MajorID);
            return View(learner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner submittedLearner)
        {
            if (id != submittedLearner.LearnerID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await PopulateMajors(submittedLearner.MajorID);
                return View(submittedLearner);
            }

            var learner = await _context.Learners.FindAsync(id);
            if (learner is null)
            {
                return NotFound();
            }

            learner.FirstMidName = submittedLearner.FirstMidName;
            learner.LastName = submittedLearner.LastName;
            learner.MajorID = submittedLearner.MajorID;
            learner.EnrollmentDate = submittedLearner.EnrollmentDate;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var learner = await _context.Learners
                .AsNoTracking()
                .Include(item => item.Major)
                .Include(item => item.Enrollments)
                    .ThenInclude(enrollment => enrollment.Course)
                .FirstOrDefaultAsync(item => item.LearnerID == id.Value);

            if (learner is null)
            {
                return NotFound();
            }

            ViewData["DeleteBlocked"] = learner.Enrollments.Count > 0;
            return View(learner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var learner = await _context.Learners
                .Include(item => item.Enrollments)
                .FirstOrDefaultAsync(item => item.LearnerID == id);

            if (learner is null)
            {
                return NotFound();
            }

            if (learner.Enrollments.Count > 0)
            {
                TempData["DeleteError"] = "Không thể xóa người học đã có đăng ký học phần.";
                return RedirectToAction(nameof(Delete), new { id });
            }

            _context.Learners.Remove(learner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateMajors(int? selectedMajorId = null)
        {
            ViewData["MajorID"] = new SelectList(
                await _context.Majors.AsNoTracking().OrderBy(major => major.MajorName).ToListAsync(),
                nameof(Major.MajorID),
                nameof(Major.MajorName),
                selectedMajorId);
        }
    }
}
