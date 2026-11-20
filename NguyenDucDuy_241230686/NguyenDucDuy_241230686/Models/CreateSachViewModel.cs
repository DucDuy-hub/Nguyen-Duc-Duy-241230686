
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace NguyenDucDuy_241230686.Models
{
    public class CreateSachViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên sách")]
        public string TenSach { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng chọn thể loại")]
        public int? MaTl { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá sách")]
        [Range(50, 1000, ErrorMessage = "Giá phải từ 50 đến 1000")]
        public decimal? Gia { get; set; }

        public IFormFile? Anh { get; set; }
    }
}
