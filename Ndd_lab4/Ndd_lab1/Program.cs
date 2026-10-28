using Microsoft.EntityFrameworkCore;
using Ndd_lab1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var schoolContext = scope.ServiceProvider.GetRequiredService<SchoolContext>();
    DbInitializer.Initialize(schoolContext);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "learner-list",
    pattern: "Admin/Learner/List",
    defaults: new { controller = "Learner", action = "Index" });

app.MapControllerRoute(
    name: "learner-add",
    pattern: "Admin/Learner/Add",
    defaults: new { controller = "Learner", action = "Create" });

app.MapControllerRoute(
    name: "student-list",
    pattern: "Admin/Student/List",
    defaults: new { controller = "Student", action = "Index" });

app.MapControllerRoute(
    name: "student-add",
    pattern: "Admin/Student/Add",
    defaults: new { controller = "Student", action = "Create" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
