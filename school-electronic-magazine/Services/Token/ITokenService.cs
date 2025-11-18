using System.Security.Claims;
using school_electronic_magazine.DTO.Requests;

namespace school_electronic_magazine.Services.Token;

public interface ITokenService
{
    string GenerateAccessToken(string userId, List<string> roles);
    string GenerateRefreshToken();

    ClaimsPrincipal? ValidateAccessToken(string token);
    string GetUserIdFromToken(string accessToken);
    List<string> GetRolesFromToken(string accessToken);

    bool IsAccessTokenValid(string token);

    Task<bool> ValidateRefreshTokenAsync(long userId, string refreshToken);
    Task<TokensResponsePayload> RotateRefreshTokenAsync(string expiredAccessToken, string oldRefreshToken);
}