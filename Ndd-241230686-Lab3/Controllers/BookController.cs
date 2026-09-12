using Microsoft.AspNetCore.Mvc;
using Ndd_241230686_Lab3.Models;
using System.Linq;

namespace Ndd_241230686_Lab3.Controllers
{
    public class BookController : Controller
    {
        protected Book book = new Book();

        public IActionResult Index(int? authorId, int? genreId)
        {
            ViewBag.authors = book.Authors;
            ViewBag.genres = book.Genres;

            var books = book.GetBookList();

            if (authorId.HasValue)
                books = books.Where(x => x.AuthorId == authorId.Value).ToList();

            if (genreId.HasValue)
                books = books.Where(x => x.GenreId == genreId.Value).ToList();

            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.authors = book.Authors;
            ViewBag.genres = book.Genres;
            return View(new Book());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.authors = book.Authors;
                ViewBag.genres = book.Genres;
                return View(model);
            }

            book.Add(model);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.authors = book.Authors;
            ViewBag.genres = book.Genres;

            var model = book.GetBookById(id);

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.authors = book.Authors;
                ViewBag.genres = book.Genres;
                return View(model);
            }

            book.Update(model);
            return RedirectToAction("Index");
        }

        public PartialViewResult PopularBook()
        {
            var books = book.GetBookList();
            return PartialView("PopularBook", books);
        }
    }
}
