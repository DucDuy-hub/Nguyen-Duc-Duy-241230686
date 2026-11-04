using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
    public class LearnerController : Controller
    {
        private SchoolContext db;

        public LearnerController(SchoolContext context)
        {
            db = context;
        }

        // Lab 6 (1): biến toàn cục chỉ định số phần tử trên 1 trang
        private int pageSize = 3;

        // 8.1 LIST (R) - Lab 6: phân trang, hiển thị trang đầu
        public IActionResult Index(int? mid)
        {
            IQueryable<Learner> learners = db.Learners;
            if (mid != null)
            {
                learners = learners.Where(l => l.MajorID == mid);
                ViewBag.mid = mid;
            }
            // tính số trang
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            // trả số trang về view để hiển thị nav-trang
            ViewBag.pageNum = pageNum;
            ViewBag.page = 1;
            // lấy dữ liệu trang đầu
            var result = learners.Include(m => m.Major)
                .OrderBy(l => l.LearnerID)
                .Take(pageSize).ToList();
            return View(result);
        }

        // Lab 6 (2): phân trang kết hợp lọc (theo chuyên ngành) và tìm kiếm (theo tên)
        public IActionResult LearnerFilter(int? mid, string? keyword, int? pageIndex)
        {
            // lấy toàn bộ learners trong dbset, chuyển về IQueryable<Learner> để query
            IQueryable<Learner> learners = db.Learners;
            // lấy chỉ số trang, nếu chỉ số trang null (hoặc <= 0) thì gán ngầm định bằng 1
            int page = (pageIndex == null || pageIndex <= 0) ? 1 : pageIndex.Value;
            // nếu có mid thì lọc learner theo mid (chuyên ngành)
            if (mid != null)
            {
                learners = learners.Where(l => l.MajorID == mid);
                // gửi mid về view để ghi lại trên nav-phân trang
                ViewBag.mid = mid;
            }
            // nếu có keyword thì tìm kiếm theo tên
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                learners = learners.Where(l => l.FirstMidName.ToLower()
                    .Contains(keyword.ToLower()));
                // gửi keyword về view để ghi trên nav-phân trang
                ViewBag.keyword = keyword;
            }
            // tính số trang
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            // gửi số trang và trang hiện tại về view để hiển thị nav-trang
            ViewBag.pageNum = pageNum;
            ViewBag.page = page;
            // chọn dữ liệu trong trang hiện tại
            var result = learners.Include(m => m.Major)
                .OrderBy(l => l.LearnerID)
                .Skip(pageSize * (page - 1))
                .Take(pageSize).ToList();
            return PartialView("LearnerTable", result);
        }

        // Lab 5 (2.2.1): lọc theo MajorID bằng AJAX (Lab 6 dùng LearnerFilter thay thế, giữ lại để tương thích)
        public IActionResult LearnerByMajorID(int mid)
        {
            var learners = db.Learners
                .Where(l => l.MajorID == mid)
                .Include(m => m.Major).ToList();
            return PartialView("LearnerTable", learners);
        }

        // 8.2 CREATE (C)
        public IActionResult Create()
        {
            // Cách 1: tự tạo List<SelectListItem>
            var majors = new List<SelectListItem>();
            foreach (var item in db.Majors)
            {
                majors.Add(new SelectListItem
                {
                    Text = item.MajorName,
                    Value = item.MajorID.ToString()
                });
            }
            ViewBag.MajorID = majors;

            // Cách 2: dùng SelectList (ghi đè cách 1, chỉ cần dùng 1 trong 2 cách)
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View(new Learner { EnrollmentDate = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            // Dựng lại SelectList để hiển thị danh sách Majors
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View(learner);
        }

        // 8.3 UPDATE (U)
        public IActionResult Edit(int id)
        {
            if (db.Learners == null)
            {
                return NotFound();
            }

            var learner = db.Learners.Find(id);
            if (learner == null)
            {
                return NotFound();
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,
            [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (id != learner.LearnerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(learner);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExists(learner.LearnerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        private bool LearnerExists(int id)
        {
            return db.Learners.Any(e => e.LearnerID == id);
        }

        // 8.4 DELETE (D)
        public IActionResult Delete(int id)
        {
            if (db.Learners == null)
            {
                return NotFound();
            }

            var learner = db.Learners.Include(l => l.Major)
                .Include(e => e.Enrollments)
                .FirstOrDefault(m => m.LearnerID == id);
            if (learner == null)
            {
                return NotFound();
            }
            if (learner.Enrollments.Count() > 0)
            {
                return Content("This learner has some enrollments, can't delete!");
            }
            return View(learner);
        }

        // POST: Learner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (db.Learners == null)
            {
                return Problem("Entity set 'Learners' is null.");
            }
            var learner = db.Learners.Find(id);
            if (learner != null)
            {
                db.Learners.Remove(learner);
            }

            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
