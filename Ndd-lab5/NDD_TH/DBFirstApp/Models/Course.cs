using System.ComponentModel.DataAnnotations;

namespace DBFirstApp.Models;

public partial class Course
{
    public int CourseId { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    public int Credits { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
