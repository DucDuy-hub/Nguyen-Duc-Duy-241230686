using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NguyenDucDuy_241230686_2.Models;

public partial class QLTaiSanContext : DbContext
{
    public QLTaiSanContext(DbContextOptions<QLTaiSanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LoaiTaiSan> LoaiTaiSans { get; set; }

    public virtual DbSet<TaiSan> TaiSans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaiSan>(entity =>
        {
            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.TaiSans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaiSan_LoaiTaiSan");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
