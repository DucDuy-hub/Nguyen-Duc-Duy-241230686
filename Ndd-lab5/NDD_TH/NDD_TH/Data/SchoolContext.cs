using Microsoft.EntityFrameworkCore;
using NDD_TH.Models;

namespace NDD_TH.Data;

public class SchoolContext(DbContextOptions<SchoolContext> options) : DbContext(options)
{
    public DbSet<Major> Majors => Set<Major>();

    public DbSet<Learner> Learners => Set<Learner>();

    public DbSet<Course> Courses => Set<Course>();

    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Major>().ToTable(nameof(Major));
        modelBuilder.Entity<Learner>().ToTable(nameof(Learner));
        modelBuilder.Entity<Course>().ToTable(nameof(Course));
        modelBuilder.Entity<Enrollment>().ToTable(nameof(Enrollment));

        modelBuilder.Entity<Major>()
            .HasMany(major => major.Learners)
            .WithOne(learner => learner.Major)
            .HasForeignKey(learner => learner.MajorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Learner>()
            .HasMany(learner => learner.Enrollments)
            .WithOne(enrollment => enrollment.Learner)
            .HasForeignKey(enrollment => enrollment.LearnerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Course>()
            .HasMany(course => course.Enrollments)
            .WithOne(enrollment => enrollment.Course)
            .HasForeignKey(enrollment => enrollment.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
