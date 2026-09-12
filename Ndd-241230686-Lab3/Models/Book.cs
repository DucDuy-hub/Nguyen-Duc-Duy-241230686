using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Ndd_241230686_Lab3.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int TotalPage { get; set; }
        public string Summary { get; set; }

        public static List<Book> Books { get; set; } = new List<Book>
        {
            new Book { Id=1, Title="Chí Phèo", AuthorId=1, GenreId=1, Image="/images/products/b1.svg", Price=500000, TotalPage=250, Summary="Tác phẩm Chí Phèo." },
            new Book { Id=2, Title="Lão Hạc", AuthorId=2, GenreId=1, Image="/images/products/b2.svg", Price=700000, TotalPage=300, Summary="Tác phẩm Lão Hạc." },
            new Book { Id=4, Title="Conan Phiêu lưu ký", AuthorId=1, GenreId=2, Image="/images/products/b3.svg", Price=550000, TotalPage=350, Summary="Truyện Conan." },
            new Book { Id=6, Title="Đường Xưa Mây Trắng", AuthorId=3, GenreId=3, Image="/images/products/b4.svg", Price=850000, TotalPage=400, Summary="Đường Xưa Mây Trắng." }
        };

        public List<Book> GetBookList()
        {
            return Books;
        }

        public Book GetBookById(int id)
        {
            return Books.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Book model)
        {
            model.Id = Books.Count == 0 ? 1 : Books.Max(x => x.Id) + 1;
            if (string.IsNullOrEmpty(model.Image))
                model.Image = "/images/products/b1.svg";
            Books.Add(model);
        }

        public void Update(Book model)
        {
            var item = GetBookById(model.Id);
            if (item == null) return;

            item.Title = model.Title;
            item.AuthorId = model.AuthorId;
            item.GenreId = model.GenreId;
            item.Price = model.Price;
            item.TotalPage = model.TotalPage;
            item.Summary = model.Summary;

            if (!string.IsNullOrEmpty(model.Image))
                item.Image = model.Image;
        }

        public List<SelectListItem> Authors { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value="1", Text="Nam Cao" },
            new SelectListItem { Value="2", Text="Ngô Tất Tố" },
            new SelectListItem { Value="3", Text="Thích Nhất Hạnh" }
        };

        public List<SelectListItem> Genres { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value="1", Text="Truyện tranh" },
            new SelectListItem { Value="2", Text="Văn học đương đại" },
            new SelectListItem { Value="3", Text="Phật học phổ thông" },
            new SelectListItem { Value="4", Text="Truyện cưới" }
        };
    }
}
