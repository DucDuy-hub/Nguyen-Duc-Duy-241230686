using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NguyenDucDuy_241230686_2.Models;

[Table("LoaiTaiSan")]
public partial class LoaiTaiSan
{
    [Key]
    public int MaLoai { get; set; }

    [StringLength(50)]
    public string TenLoai { get; set; } = null!;

    [InverseProperty("MaLoaiNavigation")]
    public virtual ICollection<TaiSan> TaiSans { get; set; } = new List<TaiSan>();
}
