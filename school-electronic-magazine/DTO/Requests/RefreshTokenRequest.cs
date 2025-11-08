namespace school_electronic_magazine.DTO.Requests;

public class RefreshTokenRequest
{
    public required string RefreshToken { get; set; } = null!;
}