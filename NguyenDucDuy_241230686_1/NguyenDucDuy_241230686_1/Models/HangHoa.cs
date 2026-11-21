using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NguyenDucDuy_241230686_1.Models;

[Table("HangHoa")]
public partial class HangHoa
{
    [Key]
    public int MaHang { get; set; }

    public int MaLoai { get; set; }

    [StringLength(50)]
    public string TenHang { get; set; } = null!;

    [Column(TypeName = "numeric(18, 0)")]
    public decimal? Gia { get; set; }

    [StringLength(100)]
    public string? Anh { get; set; }

    [ForeignKey("MaLoai")]
    [InverseProperty("HangHoas")]
    public virtual LoaiHang MaLoaiNavigation { get; set; } = null!;
}
