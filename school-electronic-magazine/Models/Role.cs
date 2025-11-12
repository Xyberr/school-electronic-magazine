using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_electronic_magazine.Models;

public class Role : BaseEntity
{
    [Required] 
    public required string Name { get; set; } = null!;
    
    [MaxLength(1024)] 
    public string? Desc { get; set; }
    
    public ICollection<User> Users { get; set; } = new List<User>();
}