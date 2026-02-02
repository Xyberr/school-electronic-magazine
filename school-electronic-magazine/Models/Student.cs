using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_electronic_magazine.Models;

public class Student : User
{
    //TODO: Проверить
    [ForeignKey("Id")]
    public User User { get; set; } = null!;
    
    [Required] 
    public required long GroupId { get; set; }
    public Group Group { get; set; } = null!;
    
    [Required] 
    public required DateTime EnterDate { get; set; }
    
    public ICollection<Grade> Grades { get; set; } = [];
}
