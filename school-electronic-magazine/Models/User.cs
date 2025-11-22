using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace school_electronic_magazine.Models;

public class User : BaseEntity
{
    [Required] 
    public required string Name { get; set; } = null!;
    
    [Required] 
    public required string Surname { get; set; } = null!;
    
    [Required] 
    public required DateTime DateOfBirth { get; set; } 
    
    [Required] 
    public required DateTime LastOnline { get; set; }
    
    [Required] 
    public required string PasswordHash { get; set; } = null!;
    
    [Required] 
    public required string Login { get; set; } = null!;
    
    [Required] 
    public long ClassId { get; set; }
    
    public ICollection<Role> Roles { get; set; } = new List<Role>();
    public ICollection<ContactInfo>? ContactInfos { get; set; }
    public ICollection<Subject>? Subjects { get; set; }
    public ICollection<Lesson>? Lessons { get; set; }
    public ICollection<Group>? Groups { get; set; }
    public ICollection<Grade>? Grades { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }

}