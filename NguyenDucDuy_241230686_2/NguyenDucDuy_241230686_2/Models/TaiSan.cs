using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NguyenDucDuy_241230686_2.Models;

[Table("TaiSan")]
public partial class TaiSan
{
    [Key]
    [Column("MaTS")]
    public int MaTs { get; set; }

    public int MaLoai { get; set; }

    [Column("TenTS")]
    [StringLength(100)]
    public string TenTs { get; set; } = null!;

    [Column(TypeName = "numeric(18, 0)")]
    public decimal? NguyenGia { get; set; }

    [StringLength(100)]
    public string? Anh { get; set; }

    [ForeignKey("MaLoai")]
    [InverseProperty("TaiSans")]
    public virtual LoaiTaiSan MaLoaiNavigation { get; set; } = null!;
}
