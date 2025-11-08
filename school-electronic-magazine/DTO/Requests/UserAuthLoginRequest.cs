using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.DTO.Requests;

public class UserAuthLoginRequest
{
    [Required] public required string Login { get; set; }
    [Required] public required string Password { get; set; }
}