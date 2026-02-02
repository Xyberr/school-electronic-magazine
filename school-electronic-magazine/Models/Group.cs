using System.ComponentModel.DataAnnotations;
using school_electronic_magazine.Models.Base;

namespace school_electronic_magazine.Models;

public class Group : BaseEntity
{
    [Required]
    public required long ClassId { get; set; }

    public int CreationYear { get; set; }
    
    public ICollection<Student> Students { get; set; } = [];
    public ICollection<SchoolClass> SchoolClasses { get; set; } = [];
    
}
