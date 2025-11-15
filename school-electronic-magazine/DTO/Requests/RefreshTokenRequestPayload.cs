namespace school_electronic_magazine.DTO.Requests;

public record RefreshTokenRequestPayload
{
    public required string RefreshToken { get; init; } = null!;
}