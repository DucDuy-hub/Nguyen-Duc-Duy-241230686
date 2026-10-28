using System.ComponentModel.DataAnnotations;

namespace Ndd_lab1.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int LearnerID { get; set; }

        [Display(Name = "Điểm")]
        [Range(0, 10, ErrorMessage = "Điểm phải nằm trong khoảng 0 đến 10.")]
        public float Grade { get; set; }

        public virtual Learner? Learner { get; set; }
        public virtual Course? Course { get; set; }
    }
}
