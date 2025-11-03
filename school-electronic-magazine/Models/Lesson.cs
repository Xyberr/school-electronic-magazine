using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class Lesson : BaseEntity
{
    [Required] public required long SubjectId { get; set; }
    [Required] public required long TeacherId { get; set; }
    [Required] public required DateTime LessonDate { get; set; }
    [Required] public required string ClassRoom { get; set; } = null!;
    [Required] public required long StudentId { get; set; }
    [Required] [MaxLength(128)] public required string Title { get; set; } = null!;
    
    public ICollection<SchoolClass>? SchoolClass { get; set; }
    public Subject Subject { get; set; }
}