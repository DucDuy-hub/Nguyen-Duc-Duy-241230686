using System.ComponentModel.DataAnnotations;

namespace Ndd_lab1.Models
{
    public class Learner
    {
        public int LearnerID { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Vui lòng nhập tên.")]
        [StringLength(50, ErrorMessage = "Tên không được vượt quá 50 ký tự.")]
        public string FirstMidName { get; set; } = string.Empty;

        [Display(Name = "Họ")]
        [Required(ErrorMessage = "Vui lòng nhập họ.")]
        [StringLength(50, ErrorMessage = "Họ không được vượt quá 50 ký tự.")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Ngày nhập học")]
        [Required(ErrorMessage = "Vui lòng chọn ngày nhập học.")]
        [Range(typeof(DateTime), "1900-01-01", "9999-12-31", ErrorMessage = "Ngày nhập học không hợp lệ.")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Chuyên ngành")]
        [Required(ErrorMessage = "Vui lòng chọn chuyên ngành.")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn chuyên ngành.")]
        public int MajorID { get; set; }

        public virtual Major? Major { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
    }
}
