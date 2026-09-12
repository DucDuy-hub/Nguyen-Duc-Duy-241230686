using System.Collections.Generic;

namespace Ndd_241230686_Lab3.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Category> GetCategoryList()
        {
            return new List<Category>
            {
                new Category { Id=1, Name="Laptop" },
                new Category { Id=2, Name="Điện thoại" },
                new Category { Id=3, Name="Máy tính bảng" },
                new Category { Id=4, Name="Tai nghe" },
                new Category { Id=5, Name="Phụ kiện" }
            };
        }
    }
}
