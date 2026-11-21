
using System.ComponentModel.DataAnnotations;

namespace NguyenDucDuy_241230686_1.Models
{
    public class HangHoaCreateViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn loại hàng")]
        public int? MaLoai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên hàng")]
        public string TenHang { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [Range(typeof(decimal), "100", "5000",
            ErrorMessage = "Giá phải từ 100 đến 5000")]
        public decimal? Gia { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên ảnh")]
        [RegularExpression(
            @"^.+\.(jpg|png|gif|tiff)$",
            ErrorMessage = "Ảnh phải có đuôi .jpg, .png, .gif hoặc .tiff")]
        public string Anh { get; set; } = "";
    }
}
