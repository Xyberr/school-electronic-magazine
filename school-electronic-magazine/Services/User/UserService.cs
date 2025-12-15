using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.DTO;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.DTO.Response;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;
using school_electronic_magazine.Repositories;
using school_electronic_magazine.Services;

namespace school_electronic_magazine.Services;

public class UserService(
    IGenericRepository<User> geneticUserRepository,
    IGenericRepository<Role> geneticRoleRepository,
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
    
    public async Task AssignRolesAsync(long userId, List<string> roles)
    {
        if (roles == null || roles.Count == 0)
            throw new InvalidOperationException("Список ролей пуст");

        var user = await geneticUserRepository.GetByIdAsync(userId);

        if (user == null)
            throw new InvalidOperationException("Пользователь не найден");

        user.Roles ??= new List<Role>();
        
        var allRoles = await geneticRoleRepository.GetAllAsync();

        foreach (var roleName in roles)
        {
            var role = allRoles.FirstOrDefault(r => r.Name == roleName);

            if (role == null)
                throw new InvalidOperationException($"Роль '{roleName}' не найдена");

            if (user.Roles.Any(r => r.Id == role.Id))
                continue;

            user.Roles.Add(role);
        }

        await geneticUserRepository.UpdateAsync(user);
        await geneticUserRepository.SaveChangesAsync();
    }

    public async Task RemoveRolesAsync(long userId, List<string> roles)
    {
        if (roles == null || roles.Count == 0)
            throw new ArgumentException("Не указаны роли, которые нужно удалить");

        var user = await geneticUserRepository
            .Query()
            .Include(user => user.Roles)
            .FirstOrDefaultAsync(user => user.Id == userId);

        if (user == null)
            throw new ArgumentException("Пользователь не найден");

        if (user.Roles == null || user.Roles.Count == 0)
            throw new InvalidOperationException("У пользователя нет ролей");

        var allRoles = await geneticRoleRepository.GetAllAsync();

        foreach (var roleName in roles)
        {
            var role = allRoles.FirstOrDefault(role => role.Name == roleName);
            if (role == null)
                throw new ArgumentException($"Роль '{roleName}' не найдена");

            var existingRole = user.Roles.FirstOrDefault(role => role.Name == roleName);
            if (existingRole == null)
                throw new InvalidOperationException($"У пользователя нет роли '{roleName}'");

            user.Roles.Remove(existingRole);
        }

        await geneticUserRepository.UpdateAsync(user);
        await geneticUserRepository.SaveChangesAsync();
    }

    public async Task RemoveUserByIdAsync(long userId)
    {
        await geneticUserRepository.DeleteAsync(userId);
        await geneticUserRepository.SaveChangesAsync();
    }
}
