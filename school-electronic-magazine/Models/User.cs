using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace school_electronic_magazine.Models;

public class User : BaseEntity
{
    [Required] public required string Name { get; set; } = null!;
    [Required] public required string Surname { get; set; } = null!;
    [Required] public required DateTime DateOfBirth { get; set; } 
    [Required] public required DateTime LastOnline { get; set; }
    [Required] public required string PasswordHash { get; set; } = null!;
    [Required] public required string Login { get; set; } = null!;
    [Required] public required long ClassId { get; set; }
    [Required] public required string Role { get; set; }
    
    //public ICollection<Role> Roles { get; set; } = new List<Role>();
    public ICollection<ContactInfo>? ContactInfos { get; set; }
    public ICollection<SchoolJournal> SchoolJournal { get; set; } = new List<SchoolJournal>();
    [NotMapped] public SchoolClass? SchoolClass { get; set; }
}