using System.ComponentModel.DataAnnotations;

namespace Ndd_lab1.Models
{
    public class Major
    {
        public int MajorID { get; set; }

        [Display(Name = "Chuyên ngành")]
        [Required(ErrorMessage = "Vui lòng nhập tên chuyên ngành.")]
        [StringLength(80, ErrorMessage = "Tên chuyên ngành không được vượt quá 80 ký tự.")]
        public string MajorName { get; set; } = string.Empty;

        public virtual ICollection<Learner> Learners { get; set; } = new HashSet<Learner>();
    }
}
