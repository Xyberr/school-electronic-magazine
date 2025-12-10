using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class SchoolClass : BaseEntity
{
    public string GroupLetter { get; set; } = null!;
    public int ClassNumber { get; set; }
    public int EducationShift { get; set; }
    public DateTime EnterDate { get; set; }

    public long ClassId { get; set; }
    public Group Group { get; set; }

    public ICollection<Lesson>? Lessons { get; set; }
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}