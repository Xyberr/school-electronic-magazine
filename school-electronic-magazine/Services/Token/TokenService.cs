using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;
using school_electronic_magazine.Services;

public class TokenService(
    IConfiguration config,
    JwtSecurityTokenHandler tokenHandler,
    IGenericRepository<RefreshToken> refreshRepository,
    IOptions<Jwt> jwtOptions
) : ITokenService

{
    public string GenerateAccessToken(string userId, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var keyBytes = Encoding.UTF8.GetBytes(jwtOptions.Value.Key);
        var signingKey = new SymmetricSecurityKey(keyBytes);

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(jwtOptions.Value.ExpiresInMinutes),
            Issuer = jwtOptions.Value.Issuer,
            Audience = jwtOptions.Value.Audience,
            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(descriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public string GenerateRefreshToken()
    {
        var bytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
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
        var tokenRecord = await refreshRepository.Query()
            .Where(RefreshToken =>
                RefreshToken.UserId == userId &&
                RefreshToken.Token == refreshToken &&
                !RefreshToken.IsRevoked &&
                RefreshToken.ExpiryDate > DateTime.UtcNow)
            .FirstOrDefaultAsync();
        
        // TODO: Убрать
        if (tokenRecord == null)
        {
            Console.WriteLine($"[TokenService] Token not valid for user {userId}");
            Console.WriteLine($"Incoming token: '{refreshToken}'");

            var userTokens = await refreshRepository.Query()
                .Where(t => t.UserId == userId)
                .ToListAsync();

            foreach (var r in userTokens)
            {
                Console.WriteLine(
                    $"Stored token: '{r.Token}', IsRevoked: {r.IsRevoked}, Expiry: {r.ExpiryDate}");
            }
        }
        // DEBUB
        
        return tokenRecord;
    }
    
    public async Task<TokensResponsePayload> RotateRefreshTokenAsync(string expiredAccessToken, string oldRefreshToken)
    {
        var principal = GetPrincipalFromExpiredToken(expiredAccessToken);
        var userIdStr = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                        ?? throw new UnauthorizedAccessException("Invalid token");
        var userId = long.Parse(userIdStr);

        var oldToken = await GetValidRefreshTokenAsync(userId, oldRefreshToken)
                       ?? throw new UnauthorizedAccessException("Invalid refresh token");

        oldToken.IsRevoked = true;
        
        var newRefreshToken = GenerateRefreshToken();
        var refreshExpiryMinutes = jwtOptions.Value.RefreshTokenExpiresInMinutes;

        var newTokenEntity = new RefreshToken
        {
            UserId = userId,
            Token = newRefreshToken,
            ExpiryDate = DateTime.SpecifyKind(DateTime.UtcNow.AddMinutes(refreshExpiryMinutes), DateTimeKind.Utc),
            IsRevoked = false
        };

        await refreshRepository.AddAsync(newTokenEntity);

        var roles = principal.Claims.Where(claim => claim.Type == ClaimTypes.Role).Select(claim => claim.Value).ToList();
        var newAccessToken = GenerateAccessToken(userIdStr, roles);

        await refreshRepository.SaveChangesAsync();
        
        return new TokensResponsePayload
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