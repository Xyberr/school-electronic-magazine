using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.DTO.Responses;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;

namespace school_electronic_magazine.Services;

public class UserService(
    IGenericRepository<User> geneticUserRepository,
    IGenericRepository<Role> geneticRoleRepository,
    IUserRepository userRepository,
    ITokenService tokenService,
    IGenericRepository<RefreshToken> geneticRefreshTokenRepository
) : IUserService
{
    public async Task<UserAuthResponsePayload> AuthorizeUserAsync(UserAuthRequestPayload payload, CancellationToken cancellationToken)
    {
        var normalizedLogin = payload?.Login?.Trim();

        var user = !string.IsNullOrWhiteSpace(normalizedLogin)
            ? await userRepository.GetUserByLoginAsync(normalizedLogin, cancellationToken)
            : null;

        if (user == null ||
            payload == null ||
            string.IsNullOrWhiteSpace(payload.Password) ||
            !BCrypt.Net.BCrypt.Verify(payload.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Неверный логин или пароль");
        }

        var roles = user.Roles.Select(role => role.Name).ToList();

        var now = DateTime.UtcNow;

        var refreshTokenValue = tokenService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshTokenValue,
            ExpiryDate = now.AddDays(30),
            IsRevoked = false,
            CreationDate = now,
            ModificationDate = now
        };

        await geneticRefreshTokenRepository
            .AddAsync(refreshTokenEntity, cancellationToken);

        await geneticRefreshTokenRepository
            .SaveChangesAsync(cancellationToken);

        var accessToken = tokenService
            .GenerateAccessToken(user.Id.ToString(), roles);

        return new UserAuthResponsePayload
        {
            Token = accessToken,
            Role = roles,
            RefreshToken = refreshTokenValue
        };
    }
    
    public async Task<User> CreateUserAsync(UserRegisterRequestPayload userDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userDto);

        var normalizedLogin = userDto.Login?.Trim();

        if (string.IsNullOrWhiteSpace(normalizedLogin))
            throw new ArgumentException("Логин не может быть пустым");

        var exists = await geneticUserRepository
            .Query()
            .AnyAsync(user => user.Login == normalizedLogin, cancellationToken);

        if (exists)
            throw new InvalidOperationException("Пользователь с таким логином уже существует");

        var now = DateTime.UtcNow;

        var user = new User
        {
            Name = userDto.Name,
            Surname = userDto.Surname,
            DateOfBirth = userDto.DateOfBirth,
            Login = normalizedLogin,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            LastOnline = now,
            CreationDate = now,
            ModificationDate = now
        };

        await geneticUserRepository.AddAsync(user, cancellationToken);

        try
        {
            await geneticUserRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new InvalidOperationException("Пользователь с таким логином уже существует");
        }

        return user;
    }
    public async Task AssignRolesAsync(long userId, List<long> roleIds, CancellationToken cancellationToken)
    {
        if (roleIds == null || roleIds.Count == 0)
            throw new InvalidOperationException("Список ролей пуст");

        var user = await geneticUserRepository
            .Query()
            .Include(user => user.Roles)
            .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);

        if (user == null)
            throw new InvalidOperationException("Пользователь не найден");

        var distinctRoleIds = roleIds.Distinct().ToList();

        var rolesToAdd = await geneticRoleRepository
            .Query()
            .Where(r => distinctRoleIds.Contains(r.Id))
            .ToListAsync(cancellationToken);
        
        if (rolesToAdd.Count != distinctRoleIds.Count)
            throw new InvalidOperationException("Одна или несколько ролей не найдены");

        var existingRoleIds = user.Roles
            .Select(role => role.Id)
            .ToHashSet();

        var newRoles = rolesToAdd
            .Where(role => !existingRoleIds.Contains(role.Id));

        foreach (var role in newRoles)
            user.Roles.Add(role);

        await geneticUserRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveRolesAsync(long userId, List<long> roleIds, CancellationToken cancellationToken)
    {
        if (roleIds == null || roleIds.Count == 0)
            throw new ArgumentException("Не указаны роли для удаления");

        var user = await geneticUserRepository
            .Query()
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user == null)
            throw new InvalidOperationException("Пользователь не найден");

        if (user.Roles == null || user.Roles.Count == 0)
            return; 

        var rolesSet = roleIds.ToHashSet();

        var rolesToRemove = user.Roles
            .Where(r => rolesSet.Contains(r.Id))
            .ToList();

        foreach (var role in rolesToRemove)
            user.Roles.Remove(role);

        await geneticUserRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveUserByIdAsync(long userId, CancellationToken cancellationToken)
    {
        await geneticUserRepository.DeleteByIdAsync(userId,cancellationToken);
        await geneticUserRepository.SaveChangesAsync(cancellationToken);
    }
}
