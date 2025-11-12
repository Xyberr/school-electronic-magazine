using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class Group : BaseEntity
{
    [Required]
    public long ClassId { get; set; }
    
    [Required] 
    public long StudentId { get; set; }
    
    public User User { get; set; } = null!;
    public ICollection<SchoolClass> SchoolClasses { get; set; } = new List<SchoolClass>();

}