using System.ComponentModel.DataAnnotations;

namespace NetCoreNDDLAB5.Models
{
    public class Product : IValidatableObject
    {
        public int Id { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150, MinimumLength = 6,
            ErrorMessage = "Tên sản phẩm phải từ 6 đến 150 ký tự")]
        public string Name { get; set; }

        [Display(Name = "Ảnh sản phẩm")]
        public string Image { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        [Range(100000, double.MaxValue,
            ErrorMessage = "Giá sản phẩm phải từ 100.000 trở lên")]
        public float Price { get; set; }

        [Display(Name = "Giá bán")]
        [Required(ErrorMessage = "Giá bán không được để trống")]
        [Range(0, double.MaxValue,
            ErrorMessage = "Giá bán không được âm")]
        public float SalePrice { get; set; }

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Mô tả không được để trống")]
        [StringLength(1500,
            ErrorMessage = "Mô tả không được vượt quá 1500 ký tự")]
        public string Description { get; set; }

        [Display(Name = "Danh mục")]
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int CategoryId { get; set; }

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (SalePrice > Price * 0.9)
            {
                yield return new ValidationResult(
                    "Giá bán phải thấp hơn giá chuẩn ít nhất 10%",
                    new[] { "SalePrice" });
            }

            if (!string.IsNullOrEmpty(Description))
            {
                string description = Description.ToLower();

                string[] words =
                {
                    "die",
                    "admin",
                    "fack"
                };

                foreach (string word in words)
                {
                    if (description.Contains(word))
                    {
                        yield return new ValidationResult(
                            "Mô tả không được chứa từ nhạy cảm",
                            new[] { "Description" });

                        break;
                    }
                }
            }
        }
    }
}