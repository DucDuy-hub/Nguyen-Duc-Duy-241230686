using Microsoft.EntityFrameworkCore;
using Ndd_lesson9.Models;

namespace Ndd_lesson9.Data;

public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Banner> Banners => Set<Banner>();
    public DbSet<Blog> Blogs => Set<Blog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(category => category.Name).IsUnique();
            entity.Property(category => category.Status).HasDefaultValue(1);
            entity.Property(category => category.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasMany(category => category.Products)
                .WithOne(product => product.Category)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(product => product.Name).IsUnique();
            entity.Property(product => product.Price).HasPrecision(12, 2);
            entity.Property(product => product.SalePrice).HasPrecision(12, 2).HasDefaultValue(0);
            entity.Property(product => product.Status).HasDefaultValue(1);
            entity.Property(product => product.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasIndex(banner => banner.Name).IsUnique();
            entity.Property(banner => banner.Status).HasDefaultValue(1);
            entity.Property(banner => banner.Priority).HasDefaultValue(0);
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasIndex(blog => blog.Name).IsUnique();
            entity.Property(blog => blog.Status).HasDefaultValue(1);
            entity.Property(blog => blog.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
}
