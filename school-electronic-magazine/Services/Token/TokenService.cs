using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Models;

namespace school_electronic_magazine.Services.Token;

public class TokenService : ITokenService
{
    private IConfiguration _config;
    static Dictionary<string, string> refreshTokens = new();

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateAccessToken(string userId, List<String> roles)
    {
        var jwtSetting = _config.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSetting["Key"]);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        };
        
        claims.AddRange(roles.Select(r=> new Claim("role", r.ToString())));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSetting["ExpiresInMinutes"])),
            Issuer = jwtSetting["Issuer"],
            Audience = jwtSetting["Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken(string userId)
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        var refreshToken = Convert.ToBase64String(randomBytes);
        
         refreshTokens[userId] = refreshToken;
        
        return refreshToken;
    }
    
    public bool CheckToken(string token)
    {
        var jwtSetting = _config.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSetting["Key"]);
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = jwtSetting["Audience"],
                ValidIssuer = jwtSetting["Issuer"],
                ClockSkew = TimeSpan.Zero
            }, out _);
            
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool ValidateRefreshToken(string userId, string refreshToken)
    {
        return refreshTokens.ContainsKey(userId) && refreshTokens[userId] == refreshToken;
    }
    
    public string GetUserIdFromToken(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(accessToken);

        var claim = token.Claims.FirstOrDefault(c =>
            c.Type == ClaimTypes.NameIdentifier ||
            c.Type == "nameid");

        if (claim == null)
            throw new InvalidOperationException("Клейм идентификатора пользователя не найден в токене");

        return claim.Value;
    }
    public List<string> GetRolesFromToken(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(accessToken);
        return jwtToken.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();
    }
}