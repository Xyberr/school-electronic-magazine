using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.DTO.Response;

public class UserAuthResponcePayload
{
    [Required] 
    public required string Token { get; set; }
    
    [Required] 
    public string RefreshToken { get; set; }
    
    public List<String> Role { get; set; }
}