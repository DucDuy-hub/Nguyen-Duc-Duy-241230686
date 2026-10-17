using System.ComponentModel.DataAnnotations;

namespace Ndd_lesson9.Models;

public class Category
{
    public int Id { get; set; }

    [Display(Name = "Tên danh mục")]
    [Required(ErrorMessage = "Vui lòng nhập tên danh mục.")]
    [StringLength(100, ErrorMessage = "Tên danh mục không được vượt quá 100 ký tự.")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Trạng thái")]
    [Range(0, 1, ErrorMessage = "Vui lòng chọn trạng thái hợp lệ.")]
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

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
