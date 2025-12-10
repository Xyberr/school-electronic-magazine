using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class Student : User
{
    [Required] 
    public required long GroupId { get; set; }
    
    [Required] 
    public required DateTime EnterDate { get; set; }

    public Group Group { get; set; } = null!;
}
