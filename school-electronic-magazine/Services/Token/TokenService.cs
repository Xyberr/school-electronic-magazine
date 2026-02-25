using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using school_electronic_magazine.DTO.Responses;
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
        return token.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();
    }

    public async Task<RefreshToken?> GetValidRefreshTokenAsync(
        long userId,
        string refreshToken,
        CancellationToken cancellationToken)
    {
        return await refreshRepository.Query()
            .Where(t =>
                t.UserId == userId &&
                t.Token == refreshToken &&
                !t.IsRevoked &&
                t.ExpiryDate > DateTime.UtcNow)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TokensResponsePayload> RotateRefreshTokenAsync(
        string expiredAccessToken,
        string oldRefreshToken,
        CancellationToken cancellationToken)
    {
        var principal = GetPrincipalFromExpiredToken(expiredAccessToken);

        var userIdStr = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                        ?? throw new UnauthorizedAccessException("Invalid token");

        var userId = long.Parse(userIdStr);

        var oldToken = await GetValidRefreshTokenAsync(
            userId,
            oldRefreshToken,
            cancellationToken)
            ?? throw new UnauthorizedAccessException("Invalid refresh token");

        oldToken.IsRevoked = true;

        var newRefreshToken = GenerateRefreshToken();
        var refreshExpiryMinutes = jwtOptions.Value.RefreshTokenExpiresInMinutes;

        var newTokenEntity = new RefreshToken
        {
            UserId = userId,
            Token = newRefreshToken,
            ExpiryDate = DateTime.UtcNow.AddMinutes(refreshExpiryMinutes),
            IsRevoked = false,
            CreationDate = DateTime.UtcNow,
            ModificationDate = DateTime.UtcNow
        };

        await refreshRepository.AddAsync(newTokenEntity, cancellationToken);

        var roles = principal.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();

        var newAccessToken = GenerateAccessToken(userIdStr, roles);

        await refreshRepository.SaveChangesAsync(cancellationToken);

        return new TokensResponsePayload
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Jwt:Key"]!));

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