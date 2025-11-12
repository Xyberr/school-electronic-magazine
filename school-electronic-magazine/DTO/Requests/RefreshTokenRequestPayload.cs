namespace school_electronic_magazine.DTO.Requests;

public class RefreshTokenRequestPayload
{
    public required string RefreshToken { get; set; } = null!;
}