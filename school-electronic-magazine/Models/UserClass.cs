using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class UserClass : BaseEntity
{
    [Required] public required long UserId {get; set;} 
    [Required] public required long ClassId {get; set;}
    
    public SchoolClass SchoolClass { get; set; } = null!;
    public User User { get; set; } = null!;
}