using System.ComponentModel.DataAnnotations;

namespace NDD_TH.Models;

public class Major
{
    public int MajorId { get; set; }

    [Required, StringLength(80)]
    public string MajorName { get; set; } = string.Empty;

    public ICollection<Learner> Learners { get; set; } = new List<Learner>();
}
