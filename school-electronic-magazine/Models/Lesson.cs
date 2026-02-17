using System.ComponentModel.DataAnnotations;
using school_electronic_magazine.Models.Base;

namespace school_electronic_magazine.Models;

public class Lesson : BaseEntity
{
    [Required] 
    public required long SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    [Required] 
    public required DateTime LessonDate { get; set; }

    [Required] 
    public required string ClassRoom { get; set; } = null!;

    [Required] 
    [MaxLength(128)] 
    public required string Title { get; set; } = null!;
    
    
    public ICollection<SchoolClass> SchoolClasses { get; set; } = [];
    
    public ICollection<User> Teachers { get; set; } = [];
    
    public ICollection<Grade> Grades { get; set; } = [];
}