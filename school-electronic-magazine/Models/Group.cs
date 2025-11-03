using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class Group : BaseEntity
{
    [Required] public required long ClassId { get; set; }
    [Required] public required long StudentId { get; set; }
    
    public User User { get; set; } = null!;
    public ICollection<SchoolClass> SchoolClasses { get; set; } = new List<SchoolClass>();

}