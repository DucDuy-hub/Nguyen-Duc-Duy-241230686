using System.ComponentModel.DataAnnotations;

namespace Ndd_lesson9.Models;

public class Product
{
    public int Id { get; set; }

    [Display(Name = "Tên sản phẩm")]
    [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Giá")]
    [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "Giá phải lớn hơn 0.")]
    public decimal Price { get; set; }

    [Display(Name = "Giá khuyến mãi")]
    [Range(typeof(decimal), "0", "999999999", ErrorMessage = "Giá khuyến mãi không được âm.")]
    public decimal SalePrice { get; set; }

    [Display(Name = "Trạng thái")]
    [Range(0, 1)]
    public int Status { get; set; } = 1;

    [Display(Name = "Danh mục")]
    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục.")]
    public int CategoryId { get; set; }

    [Display(Name = "Ngày tạo")]
    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; } = DateTime.Today;

    [Display(Name = "Hình ảnh")]
    [StringLength(100)]
    public string? Image { get; set; }

    [Display(Name = "Mô tả")]
    [StringLength(350)]
    public string? Description { get; set; }

    public Category? Category { get; set; }
}
