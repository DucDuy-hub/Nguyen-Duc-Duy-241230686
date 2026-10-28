using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ndd_lab1.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }

        [Display(Name = "Tên học phần")]
        [Required(ErrorMessage = "Vui lòng nhập tên học phần.")]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Số tín chỉ")]
        [Range(1, 10, ErrorMessage = "Số tín chỉ phải từ 1 đến 10.")]
        public int Credits { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
    }
}
