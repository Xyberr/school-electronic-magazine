using System.ComponentModel.DataAnnotations;
using school_electronic_magazine.Models.Base;

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

    public ICollection<Role> Roles { get; set; } = [];
    public ICollection<ContactInfo> ContactInfos { get; set; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    
    //Свойства навигации, специфичные для учителя (используются только в том случае, если у пользователя есть роль учителя)
    public ICollection<Subject> TeacherSubjects { get; set; } = [];
    public ICollection<Lesson> Lessons { get; set; } = [];
}