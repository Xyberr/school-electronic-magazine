using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class Subject : BaseEntity
{
    [Required] public required string Name { get; set; } = null!;
    
    public ICollection<User>? TeacherId { get; set; }
    public ICollection<Lesson>? Lesson { get; set; }
}
