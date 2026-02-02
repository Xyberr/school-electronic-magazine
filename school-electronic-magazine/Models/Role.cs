using System.ComponentModel.DataAnnotations;
using school_electronic_magazine.Models.Base;

namespace school_electronic_magazine.Models;

public class Role : BaseEntity
{
    [Required] 
    public required string Name { get; set; } = null!;
    
    [MaxLength(1024)] 
    public string? Desc { get; set; }

    public ICollection<User> Users { get; set; } = [];
}