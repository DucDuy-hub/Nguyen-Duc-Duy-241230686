using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ndd_lesson7.Models;

namespace Ndd_lesson7.Controllers
{
    
    public class NddMembersController : Controller

    {
        private static List<NddMember> nddMembers = new List<NddMember>();
        // GET: NddMembersController
        public ActionResult Index()
        {
            return View(nddMembers);
        }

        // GET: NddMembersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NddMembersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NddMembersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NddMember nddMember)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View(nddMember);
                }
                nddMembers.Add(nddMember);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NddMembersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NddMembersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NddMembersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NddMembersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
