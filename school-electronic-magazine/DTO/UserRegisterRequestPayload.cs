using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace school_electronic_magazine.DTO;

public record class UserRegisterRequestPayload
{
    [Required] public string Name { get; init; } = null!;
    [Required] public string Surname { get; init; } = null!;

    private DateTime _dateOfBirth;
    
    [Required]
    public DateTime DateOfBirth
    {
        get => _dateOfBirth; 
        init => _dateOfBirth = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
    [Required] public string Login { get; init; } = null!;
    [Required] public string Password { get; init; } = null!;
}