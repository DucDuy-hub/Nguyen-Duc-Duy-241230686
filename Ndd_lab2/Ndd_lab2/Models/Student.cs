namespace Ndd_lab2.Models;

public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Major { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string ProgramType { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
