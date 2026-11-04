using System.ComponentModel.DataAnnotations;

namespace NDD_TH.Models;

public class Student
{
    public int Id { get; set; }

    [Display(Name = "Họ và tên")]
    [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
    [StringLength(80, MinimumLength = 4, ErrorMessage = "Họ và tên phải có từ 4 đến 80 ký tự.")]
    public string? Name { get; set; }

    [Display(Name = "Email Gmail")]
    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email phải có đuôi @gmail.com.")]
    public string? Email { get; set; }

    [Display(Name = "Mật khẩu")]
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$", ErrorMessage = "Mật khẩu cần ít nhất 8 ký tự, gồm chữ hoa, chữ thường, số và ký tự đặc biệt.")]
    public string? Password { get; set; }

    [Display(Name = "Ngành học")]
    [Required(ErrorMessage = "Vui lòng chọn ngành học.")]
    public Branch? Branch { get; set; }

    [Display(Name = "Giới tính")]
    [Required(ErrorMessage = "Vui lòng chọn giới tính.")]
    public Gender? Gender { get; set; }

    [Display(Name = "Hệ đào tạo")]
    public bool IsRegular { get; set; }

    [Display(Name = "Địa chỉ")]
    [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
    [DataType(DataType.MultilineText)]
    public string? Address { get; set; }

    [Display(Name = "Ngày sinh")]
    [Required(ErrorMessage = "Vui lòng chọn ngày sinh.")]
    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "1/1/1963", "12/31/2005", ErrorMessage = "Ngày sinh phải nằm trong khoảng 01/01/1963 đến 31/12/2005.")]
    public DateTime? DateOfBirth { get; set; }

    [Display(Name = "Điểm")]
    [Required(ErrorMessage = "Vui lòng nhập điểm.")]
    [Range(0, 10, ErrorMessage = "Điểm phải từ 0.0 đến 10.0.")]
    public double? Score { get; set; }

    public string? AvatarUrl { get; set; }
}
