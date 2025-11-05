using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class SchoolClass : BaseEntity
{
    [Required][MaxLength(1)] public required string GroupLabel { get; set; }
    [Required] public required int ClassNumber { get; set; }
    [Required] public long GroupId { get; set; }
    [Required] public required DateTime EnterDate { get; set; }
    
    public ICollection<Lesson>? Lesson { get; set; }
    public Group Group { get; set; }
    public ICollection<Grade> Grade { get; set; }
}