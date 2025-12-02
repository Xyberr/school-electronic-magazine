using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class Grade : BaseEntity
{
    [Required] 
    public required long StudentId { get; set; }
    
    [Required] 
    public required long SchoolClassId { get; set; }
    
    [Required] 
    public required long LessonId { get; set; }
    
    [MaxLength(10)] 
    public required string Value { get; set; }
    
    public User User { get; set; } = null!;
    public SchoolClass SchoolClass { get; set; } = null!;
}