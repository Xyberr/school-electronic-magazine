using school_electronic_magazine.DTO.Responses;

namespace school_electronic_magazine.Services;

public interface ITokenService
{
    string GenerateAccessToken(string userId, List<string> roles);

    string GenerateRefreshToken();

    string GetUserIdFromToken(string accessToken);

    List<string> GetRolesFromToken(string accessToken);

    Task<TokensResponsePayload> RotateRefreshTokenAsync(string expiredAccessToken, string oldRefreshToken, CancellationToken cancellationToken);
}