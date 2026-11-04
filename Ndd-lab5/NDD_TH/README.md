# Lab 1–5: ASP.NET Core MVC School Management

The solution contains two MVC projects:

- `NDD_TH` is the main Code First app. It includes the Student exercises from Labs 1–3 and the School data work from Labs 4–5.
- `DBFirstApp` is the separate Database First CRUD app for the `School` database.

## Run the main app

`NDD_TH` uses SQLite by default, creates `school.db` in the project directory, and seeds sample majors, learners, courses, and enrollments on startup.

```powershell
dotnet run --project NDD_TH/NDD_TH.csproj
```

The home page links to the learner list and the student exercise. Student routes follow the lab paths:

- `/Admin/Student/List`
- `/Admin/Student/Add`

To use SQL Server for the Code First exercise, set `Database:Provider` to `SqlServer` in `NDD_TH/appsettings.json`. The sample connection string targets `localhost\SQLEXPRESS`; change it to the SQL Server instance available on your machine if needed. `EnsureCreated` creates and seeds the `School` database on first launch.

## Run the Database First app

`DBFirstApp` connects to the existing SQL Server `School` database and does not create or seed it. Start the Code First app once with SQL Server selected, then run:

```powershell
dotnet run --project DBFirstApp/DBFirstApp.csproj
```

If the database schema needs to be regenerated from SQL Server in Visual Studio's Package Manager Console, use:

```powershell
Scaffold-DbContext "Server=localhost\SQLEXPRESS;Database=School;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -Project DBFirstApp -OutputDir Models -ContextDir Data -Context SchoolContext -Force
```

## Implemented labs

- Lab 1: student list and create form, enums for branch and gender, named route paths, and optional avatar upload.
- Lab 2: shared layouts, partial views, a menu ViewComponent, `RenderBody`, and optional script sections.
- Lab 3: Vietnamese validation messages, Gmail-only email, password policy, date and score ranges.
- Lab 4: School Code First models, EF Core context, seeded relational data, learner CRUD, and delete protection when enrollments exist. `DBFirstApp` contains the Database First version.
- Lab 5: major filter ViewComponent, synchronous initial list, and AJAX filtering by major.
