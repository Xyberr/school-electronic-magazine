using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class Lesson : BaseEntity
{
    [Required] public long SubjectId { get; set; }

    [Required] public long TeacherId { get; set; }

    [Required] public DateTime LessonDate { get; set; }

    [Required] public string ClassRoom { get; set; } = null!;

    [Required] public long StudentId { get; set; }

    [Required] [MaxLength(128)] public string Title { get; set; } = null!;

    public Subject Subject { get; set; } = null!;

    public Student Student { get; set; } = null!;

    public User Teacher { get; set; } = null!;

    public ICollection<SchoolClass>? SchoolClasses { get; set; }
}