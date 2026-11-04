using DBFirstApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký SchoolContext (do Scaffold-DbContext sinh ra) với connection string trong appsettings.json
builder.Services.AddDbContext<SchoolContext>(options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));

builder.Services.AddControllersWithViews();

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
