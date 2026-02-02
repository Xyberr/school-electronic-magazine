using System.ComponentModel.DataAnnotations;
using school_electronic_magazine.Models.Base;

namespace school_electronic_magazine.Models;

public class Grade : BaseEntity
{
    [Required] 
    public required long StudentId { get; set; }
    public Student Student { get; set; } = null!;
    
    [Required] 
    public required long LessonId { get; set; }
    
    [MaxLength(10)] 
    public required string Value { get; set; }
}