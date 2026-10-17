using System.ComponentModel.DataAnnotations;

namespace Ndd_lesson9.Models;

public class Blog
{
    public int Id { get; set; }

    [Display(Name = "Tiêu đề")]
    [Required(ErrorMessage = "Vui lòng nhập tiêu đề bài viết.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Trạng thái")]
    [Range(0, 1)]
    public int Status { get; set; } = 1;

    [Display(Name = "Ngày tạo")]
    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; } = DateTime.Today;

    [Display(Name = "Hình ảnh")]
    [StringLength(100)]
    public string? Image { get; set; }

    [Display(Name = "Mô tả")]
    [StringLength(350)]
    public string? Description { get; set; }
}
