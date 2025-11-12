using Microsoft.AspNetCore.Identity.Data;
using school_electronic_magazine.Data;
using school_electronic_magazine.DTO;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.DTO.Response;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;
using school_electronic_magazine.Repositories.Users;
using school_electronic_magazine.Services.Token;

namespace school_electronic_magazine.Services.Auth;

public class UserService(IConfiguration config, IGenericRepository<User> repository, IUserRepository userRepository, ITokenService tokenService) : IUserService
{
    private readonly IConfiguration _config;
    private readonly IGenericRepository<User> _repository;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    
    public async Task<UserResponse> GetUserByLoginAsync(UserAuthRequestPayload userAuthRequestPayload)
    {
        var user = await _userRepository.GetUserByLoginAsync(userAuthRequestPayload.Login);

        if (user == null)
            throw new Exception("Пользователь не найден");
        
        if (user.PasswordHash != userAuthRequestPayload.Password)
            throw new Exception("Неверный пароль");

        var roles = user.Roles.Select(role => role.Name).ToList();

        var accessToken = _tokenService.GenerateAccessToken(user.Id.ToString(), roles);
        var refreshToken = _tokenService.GenerateRefreshToken(user.Id.ToString());

        return new UserResponse
        {
            Token = accessToken,
            Role = roles,
            RefreshToken = refreshToken
        };
    }

    public async Task<User> CreateUserAsync(UserRegisterRequestPayload userDto)
    {
        var user = new User
        {
            Name = userDto.Name,
            Surname = userDto.Surname,
            DateOfBirth = userDto.DateOfBirth,
            PasswordHash = userDto.Password,
            Login = userDto.Login,
            LastOnline = DateTime.UtcNow,
            CreationDate = DateTime.UtcNow
        };
        
        return await _repository.AddAsync(user, true);
    }
}