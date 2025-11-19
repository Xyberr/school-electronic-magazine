using school_electronic_magazine.DTO;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.DTO.Response;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;
using school_electronic_magazine.Repositories.Users;
using school_electronic_magazine.Services.Token;

namespace school_electronic_magazine.Services.Auth;

public class UserService(
    IGenericRepository<User> geneticUserRepository,
    IUserRepository userRepository,
    ITokenService tokenService,
    IGenericRepository<RefreshToken> geneticRefreshTokenRepository
) : IUserService
{
    public async Task<UserAuthResponcePayload> AuthorizeUserAsync(UserAuthRequestPayload payload)
    {
        if (payload == null || string.IsNullOrWhiteSpace(payload.Login))
            throw new UnauthorizedAccessException("Неверный логин или пароль");

        var user = await userRepository.GetUserByLoginAsync(payload.Login.Trim());
        if (user == null || !BCrypt.Net.BCrypt.Verify(payload.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Неверный логин или пароль");

        var roles = user.Roles?.Select(r => r.Name).ToList() ?? new List<string>();

        var refreshTokenEntity = new Models.RefreshToken
        {
            UserId = user.Id,
            Token = tokenService.GenerateRefreshToken(),
            ExpiryDate = DateTime.UtcNow.AddDays(30),
            IsRevoked = false
        };

        await geneticRefreshTokenRepository.AddAsync(refreshTokenEntity);
        await geneticRefreshTokenRepository.SaveChangesAsync();

        var accessToken = tokenService.GenerateAccessToken(user.Id.ToString(), roles);

        return new UserAuthResponcePayload
        {
            Token = accessToken,
            Role = roles,
            RefreshToken = refreshTokenEntity.Token
        };
    }

    public async Task<User> CreateUserAsync(UserRegisterRequestPayload userDto)
    {
        if (userDto == null)
            throw new ArgumentNullException(nameof(userDto));

        var existingUser = await userRepository.GetUserByLoginAsync(userDto.Login.Trim());
        if (existingUser != null)
            throw new InvalidOperationException("Пользователь с таким логином, уже существует");

        var user = new User
        {
            Name = userDto.Name,
            Surname = userDto.Surname,
            DateOfBirth = userDto.DateOfBirth,
            Login = userDto.Login.Trim(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            LastOnline = DateTime.UtcNow,
            CreationDate = DateTime.UtcNow
        };

        await geneticUserRepository.AddAsync(user);
        await geneticUserRepository.SaveChangesAsync();
        return user;
    }
}
