using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.DTO;

public class UserDTO
{
    [Required] public required string Name { get; set; } = null!;
    [Required] public required string Surname { get; set; } = null!;
    [Required] public required DateTime DateOfBirth { get; set; }
    [Required] public required string Login { get; set; } = null;
    [Required] public required string PasswordHash { get; set; } = null!;
}