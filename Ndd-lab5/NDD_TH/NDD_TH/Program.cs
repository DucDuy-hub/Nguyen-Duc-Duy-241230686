using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using NDD_TH.Data;
using NDD_TH.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<StudentStore>();

var databaseProvider = builder.Configuration["Database:Provider"] ?? "Sqlite";
var connectionName = databaseProvider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase)
    ? "SchoolSqlServer"
    : "SchoolContext";
var connectionString = builder.Configuration.GetConnectionString(connectionName)
    ?? throw new InvalidOperationException($"Connection string '{connectionName}' is missing.");

if (!databaseProvider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
{
    var sqliteConnection = new SqliteConnectionStringBuilder(connectionString);
    if (sqliteConnection.DataSource != ":memory:"
        && !sqliteConnection.DataSource.StartsWith("file:", StringComparison.OrdinalIgnoreCase)
        && !Path.IsPathRooted(sqliteConnection.DataSource))
    {
        sqliteConnection.DataSource = Path.GetFullPath(sqliteConnection.DataSource, builder.Environment.ContentRootPath);
    }

    connectionString = sqliteConnection.ToString();
}

builder.Services.AddDbContext<SchoolContext>(options =>
{
    if (databaseProvider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlServer(connectionString);
    }
    else
    {
        options.UseSqlite(connectionString);
    }
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SchoolContext>();
    DbInitializer.Initialize(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "student-list",
    pattern: "Admin/Student/List",
    defaults: new { controller = "Student", action = "Index" })
    .WithStaticAssets();

app.MapControllerRoute(
    name: "student-add",
    pattern: "Admin/Student/Add",
    defaults: new { controller = "Student", action = "Create" })
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
