using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDD_TH.Models;

public class Course
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CourseId { get; set; }

    [Required, StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Range(1, 10)]
    public int Credits { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
