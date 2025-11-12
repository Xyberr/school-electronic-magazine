using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.DTO.Requests;

public record UserAuthRequestPayload
{
    [Required]
    public string Login { get; init; }
    
    [Required]
    public string Password { get; init; }
}