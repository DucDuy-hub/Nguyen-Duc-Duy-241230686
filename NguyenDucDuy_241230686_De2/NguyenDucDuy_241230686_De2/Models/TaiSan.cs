using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NguyenDucDuy_241230686_De2.Models;

[Table("TaiSan")]
public partial class TaiSan
{
    [Key]
    [Column("MaTS")]
    public int MaTs { get; set; }

    public int MaLoai { get; set; }

    [Column("TenTS")]
    [StringLength(100)]
    [Required(ErrorMessage = "Tên tài sản không được để trống")]
    public string TenTs { get; set; } = null!;

    [Column(TypeName = "numeric(18, 0)")]
    [Range(1, 2000,ErrorMessage = "Nguyên giá phải từ 1 đến 2000 triệu")]
    public decimal? NguyenGia { get; set; }

    [StringLength(100)]

    [RegularExpression(@"^.+\.(jpg|jpeg|png|webp)$",ErrorMessage = "Ảnh phải có định dạng jpg, jpeg, png hoặc webp")]
    public string? Anh { get; set; }


    [ForeignKey("MaLoai")]
    [InverseProperty("TaiSans")]
    public virtual LoaiTaiSan MaLoaiNavigation { get; set; } = null!;
}
