using System.ComponentModel.DataAnnotations;

namespace Ndd_lesson9.Models;

public class Banner
{
    public int Id { get; set; }

    [Display(Name = "Tên banner")]
    [Required(ErrorMessage = "Vui lòng nhập tên banner.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Trạng thái")]
    [Range(0, 1)]
    public int Status { get; set; } = 1;

    [Display(Name = "Thứ tự ưu tiên")]
    [Range(0, int.MaxValue)]
    public int Priority { get; set; }

    [Display(Name = "Hình ảnh")]
    [StringLength(100)]
    public string? Image { get; set; }

    [Display(Name = "Mô tả")]
    [StringLength(350)]
    public string? Description { get; set; }
}
