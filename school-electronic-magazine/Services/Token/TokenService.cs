using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;
using school_electronic_magazine.Services.Token;

public class TokenService(IConfiguration config,JwtSecurityTokenHandler tokenHandler ,IGenericRepository<RefreshToken> refreshRepository) : ITokenService
{
    public string GenerateAccessToken(string userId, List<string> roles)
    {
        var jwtSection = config.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSection["Key"]!);

        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSection["ExpiresInMinutes"]!)),
            Issuer = jwtSection["Issuer"],
            Audience = jwtSection["Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public string GenerateRefreshToken()
    {
        var bytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
    
    public ClaimsPrincipal? ValidateAccessToken(string token)
    {
        var jwtSection = config.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSection["Key"]!);

        var validationParams = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSection["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            return tokenHandler.ValidateToken(token, validationParams, out _);
        }
        catch (SecurityTokenException)
        {
            return null;
        }
        catch (Exception)
        {
            return null; 
        }
    }

    public bool IsAccessTokenValid(string token) => ValidateAccessToken(token) != null;

    public string GetUserIdFromToken(string accessToken)
    {
        var token = tokenHandler.ReadJwtToken(accessToken);
        return token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
               ?? throw new InvalidOperationException("UserId claim not found in token");
    }
    public List<string> GetRolesFromToken(string accessToken)
    {
        var token = tokenHandler.ReadJwtToken(accessToken);
        return token.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
    }
    public async Task<RefreshToken?> GetValidRefreshTokenAsync(long userId, string refreshToken)
    {
        var allTokens = await refreshRepository.GetAllAsync();

        var tokenRecord = allTokens.FirstOrDefault(r =>
            r.UserId == userId &&
            r.Token == refreshToken &&
            !r.IsRevoked &&
            r.ExpiryDate > DateTime.UtcNow);

        if (tokenRecord == null)
        {
            Console.WriteLine($"[TokenService] Token not valid for user {userId}");
            Console.WriteLine($"Incoming token: '{refreshToken}'");
            foreach (var r in allTokens.Where(t => t.UserId == userId))
            {
                Console.WriteLine($"Stored token: '{r.Token}', IsRevoked: {r.IsRevoked}, Expiry: {r.ExpiryDate}");
            }
        }

        return tokenRecord;
    }
    public async Task<bool> ValidateRefreshTokenAsync(long userId, string refreshToken)
    {
        var tokenRecord = await GetValidRefreshTokenAsync(userId, refreshToken);
        return tokenRecord != null;
    }
    
    public async Task<TokensRequestPayload> RotateRefreshTokenAsync(string expiredAccessToken, string oldRefreshToken)
    {
        var principal = GetPrincipalFromExpiredToken(expiredAccessToken);
        var userIdStr = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                        ?? throw new UnauthorizedAccessException("Invalid token");
        var userId = long.Parse(userIdStr);

        var oldToken = await GetValidRefreshTokenAsync(userId, oldRefreshToken)
                       ?? throw new UnauthorizedAccessException("Invalid refresh token");

        oldToken.IsRevoked = true;
        oldToken.ExpiryDate = DateTime.SpecifyKind(oldToken.ExpiryDate, DateTimeKind.Utc);

        await refreshRepository.UpdateAsync(oldToken);

        var jwtSection = config.GetSection("Jwt");
        var newRefreshToken = GenerateRefreshToken();
        var refreshExpiryMinutes = int.Parse(jwtSection["RefreshTokenExpiresInMinutes"]!);

        var newTokenEntity = new RefreshToken
        {
            UserId = userId,
            Token = newRefreshToken,
            ExpiryDate = DateTime.SpecifyKind(DateTime.UtcNow.AddMinutes(refreshExpiryMinutes), DateTimeKind.Utc),
            IsRevoked = false
        };

        await refreshRepository.AddAsync(newTokenEntity);
        await refreshRepository.SaveChangesAsync();

        var roles = principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        var newAccessToken = GenerateAccessToken(userIdStr, roles);

        return new TokensRequestPayload
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));

        var validationParams = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateLifetime = false
        };

        return tokenHandler.ValidateToken(token, validationParams, out _);
    }
}
