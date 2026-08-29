using Microsoft.AspNetCore.Mvc;
using MyAppMVC.Models;

namespace MyAppMVC.Controllers;

public class ProductController : Controller
{
    private List<Category> GetCategories() => new()
    {
        new() { Id = 1, Name = "Quần áo" },
        new() { Id = 2, Name = "Đồ chơi" },
        new() { Id = 3, Name = "Túi xách" },
        new() { Id = 4, Name = "Thời trang" }
    };

    private List<Product> GetProducts() => new()
    {
        new() { Id=1, Name="Bộ đồ chơi cho trẻ em", Image="/images/products/product1.jpg", Price=350000, SalePrice=299000, CategoryId=2, Description="Bộ đồ chơi dành cho trẻ em, an toàn và nhiều màu sắc.", Status=true, CreatedAt=new DateTime(2026,8,1) },
        new() { Id=2, Name="Bộ đồ bơi cho trẻ em", Image="/images/products/product2.jpg", Price=450000, SalePrice=399000, CategoryId=1, Description="Bộ đồ bơi trẻ em với chất liệu thoải mái.", Status=true, CreatedAt=new DateTime(2026,8,2) },
        new() { Id=3, Name="Bộ đồ bơi trẻ em 3-5 tuổi", Image="/images/products/product3.jpg", Price=400000, SalePrice=350000, CategoryId=1, Description="Đồ bơi dành cho trẻ từ 3 đến 5 tuổi.", Status=true, CreatedAt=new DateTime(2026,8,3) },
        new() { Id=4, Name="Bộ đồ thể thao", Image="/images/products/product4.jpg", Price=500000, SalePrice=450000, CategoryId=1, Description="Bộ đồ thể thao thời trang và thoải mái.", Status=true, CreatedAt=new DateTime(2026,8,4) },
        new() { Id=5, Name="Túi thời trang nữ", Image="/images/products/product5.jpg", Price=800000, SalePrice=650000, CategoryId=3, Description="Túi thời trang nữ kiểu dáng hiện đại.", Status=true, CreatedAt=new DateTime(2026,8,5) },
        new() { Id=6, Name="Túi thời trang da", Image="/images/products/product6.jpg", Price=1200000, SalePrice=990000, CategoryId=3, Description="Túi da thời trang cao cấp.", Status=true, CreatedAt=new DateTime(2026,8,6) }
    };

    public IActionResult Index()
    {
        ViewBag.Products = GetProducts();
        ViewBag.Categories = GetCategories();
        return View();
    }

    public IActionResult Category(int id)
    {
        ViewBag.Products = GetProducts().Where(p => p.CategoryId == id).ToList();
        ViewBag.Categories = GetCategories();
        return View("Index");
    }

    public IActionResult Detail(int id)
    {
        var product = GetProducts().FirstOrDefault(p => p.Id == id);
        return product is null ? NotFound() : View(product);
    }
}
