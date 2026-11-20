using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NguyenDucDuy_241230686.Models;

public partial class QLNhaSachContext : DbContext
{
    public QLNhaSachContext(DbContextOptions<QLNhaSachContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<TheLoai> TheLoais { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasOne(d => d.MaTlNavigation).WithMany(p => p.Saches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sach_TheLoai");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
