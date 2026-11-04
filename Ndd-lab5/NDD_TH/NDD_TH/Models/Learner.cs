using System.ComponentModel.DataAnnotations;

namespace NDD_TH.Models;

public class Learner
{
    public int LearnerId { get; set; }

    [Display(Name = "Tên")]
    [Required(ErrorMessage = "Vui lòng nhập tên.")]
    [StringLength(80, ErrorMessage = "Tên không được vượt quá 80 ký tự.")]
    public string FirstMidName { get; set; } = string.Empty;

    [Display(Name = "Họ")]
    [Required(ErrorMessage = "Vui lòng nhập họ.")]
    [StringLength(80, ErrorMessage = "Họ không được vượt quá 80 ký tự.")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Ngày nhập học")]
    [Required(ErrorMessage = "Vui lòng chọn ngày nhập học.")]
    [DataType(DataType.Date)]
    public DateTime EnrollmentDate { get; set; }

    [Display(Name = "Ngành học")]
    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn ngành học.")]
    public int MajorId { get; set; }

    public Major? Major { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
