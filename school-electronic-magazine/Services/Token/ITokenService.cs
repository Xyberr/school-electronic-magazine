using school_electronic_magazine.DTO.Requests;

namespace school_electronic_magazine.Services.Token;

public interface ITokenService
{
    public string GenerateAccessToken(string userId, List<String> roles);
    public string GenerateRefreshToken(string userId);
    public bool CheckToken(string token);
    bool ValidateRefreshToken(string userId, string refreshToken);
    string GetUserIdFromToken(string accessToken);
    List<string> GetRolesFromToken(string accessToken);
}