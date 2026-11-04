using DBFirstApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DBFirstApp.Data;

// Generated-style reverse-engineered context for the School database.
public partial class SchoolContext(DbContextOptions<SchoolContext> options) : DbContext(options)
{
    public virtual DbSet<Course> Courses => Set<Course>();

    public virtual DbSet<Enrollment> Enrollments => Set<Enrollment>();

    public virtual DbSet<Learner> Learners => Set<Learner>();

    public virtual DbSet<Major> Majors => Set<Major>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Major>(entity =>
        {
            entity.ToTable("Major");
            entity.HasKey(item => item.MajorId);
            entity.Property(item => item.MajorName).HasMaxLength(80);
        });

        modelBuilder.Entity<Learner>(entity =>
        {
            entity.ToTable("Learner");
            entity.HasKey(item => item.LearnerId);
            entity.Property(item => item.FirstMidName).HasMaxLength(80);
            entity.Property(item => item.LastName).HasMaxLength(80);
            entity.HasOne(item => item.Major)
                .WithMany(item => item.Learners)
                .HasForeignKey(item => item.MajorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Course");
            entity.HasKey(item => item.CourseId);
            entity.Property(item => item.CourseId).ValueGeneratedNever();
            entity.Property(item => item.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.ToTable("Enrollment");
            entity.HasKey(item => item.EnrollmentId);
            entity.HasOne(item => item.Course)
                .WithMany(item => item.Enrollments)
                .HasForeignKey(item => item.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(item => item.Learner)
                .WithMany(item => item.Enrollments)
                .HasForeignKey(item => item.LearnerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
