using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.DTO.Response;

public record UserAuthResponcePayload
{
    [Required] 
    public required string Token { get; init; }
    
    [Required] 
    public string RefreshToken { get; init; }
    
    public List<String> Role { get; init; }
}