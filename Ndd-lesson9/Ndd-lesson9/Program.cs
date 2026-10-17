using Microsoft.EntityFrameworkCore;
using Ndd_lesson9.Data;
using Ndd_lesson9.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("StoreDatabase") ?? "Data Source=store.db"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await InitializeDatabaseAsync(app);

app.Run();

static async Task InitializeDatabaseAsync(WebApplication app)
{
    await using var scope = app.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
    await db.Database.EnsureCreatedAsync();

    if (!await db.Categories.AnyAsync())
    {
        db.Categories.AddRange(
            new Category { Name = "Cà phê", Description = "Hương vị cà phê rang xay mỗi ngày", Image = "/images/coffee.svg" },
            new Category { Name = "Trà", Description = "Những lựa chọn trà dịu nhẹ", Image = "/images/tea.svg" },
            new Category { Name = "Bánh ngọt", Description = "Bánh tươi dùng kèm đồ uống", Image = "/images/pastry.svg" });
        await db.SaveChangesAsync();
    }

    var categories = await db.Categories.OrderBy(category => category.Id).ToListAsync();
    if (!await db.Products.AnyAsync() && categories.Count > 0)
    {
        db.Products.AddRange(
            new Product { Name = "Cà phê sữa đá", Price = 39000, SalePrice = 35000, CategoryId = categories[0].Id, Image = "/images/coffee.svg", Description = "Cà phê đậm vị cùng sữa đặc." },
            new Product { Name = "Trà đào cam sả", Price = 45000, CategoryId = categories[Math.Min(1, categories.Count - 1)].Id, Image = "/images/tea.svg", Description = "Trà trái cây thanh mát." },
            new Product { Name = "Bánh croissant", Price = 42000, CategoryId = categories[Math.Min(2, categories.Count - 1)].Id, Image = "/images/pastry.svg", Description = "Bánh nướng bơ thơm giòn." });
        await db.SaveChangesAsync();
    }

    if (!await db.Banners.AnyAsync())
    {
        db.Banners.Add(new Banner
        {
            Name = "Chào ngày mới",
            Priority = 1,
            Image = "/images/coffee.svg",
            Description = "Một tách cà phê ngon và một khoảng nghỉ vừa đủ."
        });
        await db.SaveChangesAsync();
    }

    if (!await db.Blogs.AnyAsync())
    {
        db.Blogs.Add(new Blog
        {
            Name = "Bí quyết chọn cà phê rang xay",
            Image = "/images/coffee.svg",
            Description = "Tìm hiểu cách chọn hạt cà phê phù hợp với khẩu vị và phương pháp pha yêu thích."
        });
        await db.SaveChangesAsync();
    }
}
