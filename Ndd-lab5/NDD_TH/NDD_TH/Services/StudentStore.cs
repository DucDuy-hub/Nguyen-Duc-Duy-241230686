using NDD_TH.Models;

namespace NDD_TH.Services;

public class StudentStore
{
    private readonly object _gate = new();
    private readonly List<Student> _students =
    [
        new Student { Id = 101, Name = "Hải Nam", Branch = Branch.IT, Gender = Gender.Male, IsRegular = true, Email = "nam@gmail.com", Address = "A1-2018", DateOfBirth = new DateTime(2002, 12, 1), Score = 8.2 },
        new Student { Id = 102, Name = "Tuấn", Branch = Branch.BE, Gender = Gender.Female, IsRegular = true, Email = "tuan@gmail.com", Address = "A1-2019", DateOfBirth = new DateTime(2002, 12, 1), Score = 7.5 },
        new Student { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE, Gender = Gender.Male, IsRegular = false, Email = "phong@gmail.com", Address = "A1-2020", DateOfBirth = new DateTime(2003, 6, 15), Score = 8.8 },
        new Student { Id = 104, Name = "Xuân Mai", Branch = Branch.EE, Gender = Gender.Female, IsRegular = false, Email = "mai@gmail.com", Address = "A1-2021", DateOfBirth = new DateTime(2004, 3, 20), Score = 9.1 }
    ];

    private int _nextId = 105;

    public IReadOnlyList<Student> GetAll()
    {
        lock (_gate)
        {
            return _students
                .OrderBy(student => student.Id)
                .Select(student => new Student
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Branch = student.Branch,
                    Gender = student.Gender,
                    IsRegular = student.IsRegular,
                    Address = student.Address,
                    DateOfBirth = student.DateOfBirth,
                    Score = student.Score,
                    AvatarUrl = student.AvatarUrl
                })
                .ToList();
        }
    }

    public Student Add(Student student)
    {
        lock (_gate)
        {
            student.Id = _nextId++;
            student.Password = null;
            _students.Add(student);
            return student;
        }
    }
}
