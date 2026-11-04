using System.ComponentModel.DataAnnotations;

namespace NDD_TH.Models;

public class Enrollment
{
    public int EnrollmentId { get; set; }

    public int CourseId { get; set; }

    public int LearnerId { get; set; }

    [Range(0, 10)]
    public float Grade { get; set; }

    public Learner? Learner { get; set; }

    public Course? Course { get; set; }
}
