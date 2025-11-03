using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using school_electronic_magazine.Models;

namespace school_electronic_magazine.DTO;

public class UserDto
{
    [Required] public required string Name { get; set; } = null!;
    [Required] public required string Surname { get; set; } = null!;
    [Required] public required DateTime DateOfBirth { get; set; } 
    [Required] public required string PasswordHash { get; set; } = null!;
    [Required] public required string Login { get; set; } = null!;
    [Required] public required long ClassId { get; set; }
    public List<string> Roles { get; set; }
}