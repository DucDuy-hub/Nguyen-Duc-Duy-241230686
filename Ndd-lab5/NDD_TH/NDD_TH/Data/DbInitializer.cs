using Microsoft.EntityFrameworkCore;
using NDD_TH.Models;

namespace NDD_TH.Data;

public static class DbInitializer
{
    public static void Initialize(SchoolContext context)
    {
        context.Database.EnsureCreated();

        if (context.Majors.Any())
        {
            return;
        }

        var it = new Major { MajorName = "IT" };
        var economics = new Major { MajorName = "Economics" };
        var mathematics = new Major { MajorName = "Mathematics" };
        context.Majors.AddRange(it, economics, mathematics);
        context.SaveChanges();

        var carson = new Learner { FirstMidName = "Carson", LastName = "Alexander", EnrollmentDate = new DateTime(2005, 9, 1), Major = it };
        var meredith = new Learner { FirstMidName = "Meredith", LastName = "Alonso", EnrollmentDate = new DateTime(2005, 9, 1), Major = economics };
        var tuanNam = new Learner { FirstMidName = "Tuấn Nam", LastName = "Trần", EnrollmentDate = new DateTime(2023, 10, 4), Major = it };
        context.Learners.AddRange(carson, meredith, tuanNam);
        context.SaveChanges();

        var chemistry = new Course { CourseId = 1050, Title = "Chemistry", Credits = 3 };
        var microeconomics = new Course { CourseId = 4022, Title = "Microeconomics", Credits = 3 };
        var macroeconomics = new Course { CourseId = 4041, Title = "Macroeconomics", Credits = 3 };
        var calculus = new Course { CourseId = 1045, Title = "Calculus", Credits = 4 };
        var trigonometry = new Course { CourseId = 3141, Title = "Trigonometry", Credits = 4 };
        context.Courses.AddRange(chemistry, microeconomics, macroeconomics, calculus, trigonometry);
        context.SaveChanges();

        context.Enrollments.AddRange(
            new Enrollment { Learner = carson, Course = chemistry, Grade = 5.5f },
            new Enrollment { Learner = carson, Course = microeconomics, Grade = 7f },
            new Enrollment { Learner = meredith, Course = chemistry, Grade = 8f },
            new Enrollment { Learner = tuanNam, Course = trigonometry, Grade = 8.5f });
        context.SaveChanges();
    }
}
