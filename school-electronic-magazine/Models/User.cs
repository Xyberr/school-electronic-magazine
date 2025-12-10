using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace school_electronic_magazine.Models;

public class User : BaseEntity
{
    [Required] public string Name { get; set; } = null!;
    [Required] public string Surname { get; set; } = null!;
    [Required] public DateTime DateOfBirth { get; set; }
    [Required] public DateTime LastOnline { get; set; }
    [Required] public string PasswordHash { get; set; } = null!;
    [Required] public string Login { get; set; } = null!;

    public ICollection<Role> Roles { get; set; } = new List<Role>();
    public ICollection<ContactInfo>? ContactInfos { get; set; }
    public ICollection<Subject>? TeacherSubjects { get; set; }
    public ICollection<Lesson>? Lessons { get; set; }
    public ICollection<Grade>? Grades { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
}