using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.DTO.Responses;

public record TokensResponsePayload
{
    [Required]
    public string RefreshToken { get; init; }
    
    [Required]
    public string AccessToken { get; init; }
}