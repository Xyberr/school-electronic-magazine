using school_electronic_magazine.DTO.Responses;

namespace school_electronic_magazine.Services;

public interface ITokenService
{
    string GenerateAccessToken(string userId, List<string> roles, CancellationToken cancellationToken);
    string GenerateRefreshToken(CancellationToken cancellationToken);
    string GetUserIdFromToken(string accessToken, CancellationToken cancellationToken);
    List<string> GetRolesFromToken(string accessToken, CancellationToken cancellationToken);
    Task<TokensResponsePayload> RotateRefreshTokenAsync(string expiredAccessToken, string oldRefreshToken, CancellationToken cancellationToken);
}