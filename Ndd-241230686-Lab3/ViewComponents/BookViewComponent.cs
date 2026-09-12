using Microsoft.AspNetCore.Mvc;
using Ndd_241230686_Lab3.Models;

namespace Ndd_241230686_Lab3.ViewComponents
{
    public class BookViewComponent : ViewComponent
    {
        protected Book book = new Book();

        public IViewComponentResult Invoke()
        {
            var books = book.GetBookList();
            return View(books);
        }
    }
}
