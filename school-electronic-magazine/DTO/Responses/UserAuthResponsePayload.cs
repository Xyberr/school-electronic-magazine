using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.DTO.Responses;

public record UserAuthResponsePayload
{
    [Required] 
    public required string Token { get; init; }
    
    [Required] 
    public string RefreshToken { get; init; }
    
    public List<string> Role { get; init; }
}