using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NguyenDucDuy_241230686.Models;

[Table("Sach")]
public partial class Sach
{
    [Key]
    public int MaSach { get; set; }

    [Column("MaTL")]
    public int MaTl { get; set; }

    [StringLength(100)]
    public string TenSach { get; set; } = null!;

    [Column(TypeName = "numeric(18, 0)")]
    public decimal? Gia { get; set; }

    [StringLength(100)]
    public string? Anh { get; set; }

    [ForeignKey("MaTl")]
    [InverseProperty("Saches")]
    public virtual TheLoai MaTlNavigation { get; set; } = null!;
}
