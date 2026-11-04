using System.ComponentModel.DataAnnotations;

namespace DBFirstApp.Models;

public partial class Major
{
    public int MajorId { get; set; }

    [Required]
    [StringLength(80)]
    public string MajorName { get; set; } = string.Empty;

    public virtual ICollection<Learner> Learners { get; set; } = new List<Learner>();
}
