using System.ComponentModel.DataAnnotations;

namespace DBFirstApp.Models;

public partial class Learner
{
    public int LearnerId { get; set; }

    [Required]
    [StringLength(80)]
    public string FirstMidName { get; set; } = string.Empty;

    [Required]
    [StringLength(80)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateTime EnrollmentDate { get; set; }

    [Range(1, int.MaxValue)]
    public int MajorId { get; set; }

    public virtual Major? Major { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
