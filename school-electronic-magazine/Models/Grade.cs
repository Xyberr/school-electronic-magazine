using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class Grade : BaseEntity
{
    [Required] public long StudentId { get; set; }
    [Required] public long SchoolClassId { get; set; }
    [Required] public long LessonId { get; set; }
    [MaxLength(10)]public string Value { get; set; }
    
    public User User { get; set; } = null!;
    public SchoolClass SchoolClass { get; set; } = null!;
}