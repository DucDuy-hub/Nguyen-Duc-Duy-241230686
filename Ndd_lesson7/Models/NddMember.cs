using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Ndd_lesson7.Models
{
    public class NddMember
    {
        public int Id { get; set; }
        [DisplayName("Tên tài khoản")]
        [Required(ErrorMessage = "Tài khoản k đc để trống")]
        [StringLength(20, MinimumLength = 3,ErrorMessage ="Tên tài khoản phải từ 3 đến 20 ký tự")]
        

        public string NddUserName { get; set; }

        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu k đc để trống")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu ít nhất 8 ký tự")]

        public string NddPassword { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email k đc để trống")]
        [DataType(DataType.EmailAddress)]
        public string NddEmail { get; set; }

        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại k đc để trống")]
        [RegularExpression(@"^0\d{9,9}$", ErrorMessage = "Số điện thoại phải 10 số, bắt đầu bằng số 0")]
        public string NddPhone { get; set; }
    }
}
