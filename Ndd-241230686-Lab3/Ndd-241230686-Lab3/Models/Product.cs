using System.Collections.Generic;

namespace Ndd_241230686_Lab3.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }

        public static List<Product> Products { get; set; } = new List<Product>
        {
            new Product { Id=1, Name="Laptop Dell", Image="/images/products/p1.svg", Price=15000000 },
            new Product { Id=2, Name="Laptop Asus", Image="/images/products/p2.svg", Price=18000000 },
            new Product { Id=3, Name="Laptop Lenovo", Image="/images/products/p3.svg", Price=17000000 },
            new Product { Id=4, Name="Laptop HP", Image="/images/products/p4.svg", Price=16000000 },
            new Product { Id=5, Name="MacBook Air", Image="/images/products/p5.svg", Price=22000000 },
            new Product { Id=6, Name="MacBook Pro", Image="/images/products/p6.svg", Price=30000000 }
        };

        public List<Product> GetProductList()
        {
            return Products;
        }
    }
}
