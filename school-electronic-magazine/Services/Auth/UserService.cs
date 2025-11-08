using Microsoft.AspNetCore.Identity.Data;
using school_electronic_magazine.Data;
using school_electronic_magazine.DTO;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.DTO.Response;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;
using school_electronic_magazine.Repositories.User;
using school_electronic_magazine.Services.Token;

namespace school_electronic_magazine.Services.Auth;

public class UserService : IUserService
{
    private readonly IConfiguration _config;
    private readonly IGenericRepository<User> _repository;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public UserService(IConfiguration config, IGenericRepository<User> repository, IUserRepository userRepository, ITokenService tokenService)
    {
        _config = config;
        _repository = repository;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<ServiceResponse<UserResponse>> GetUserByLoginAndPasswordAsync(UserAuthLoginRequest userAuthLoginRequest)
    {
        var user = await _userRepository.GetUserByLoginAndPasswordAsync(userAuthLoginRequest);
        
        if (user == null)
        {
            return new ServiceResponse<UserResponse>
            {
                Success = false,
                Message = "Пользователь не найден"
            };
        }

        var roles = user.Roles.Select(r => r.Name).ToList();
        
        var accessToken = _tokenService.GenerateAccessToken(user.Id.ToString(), roles);
        var refreshToken = _tokenService.GenerateRefreshToken(user.Id.ToString());
        
        var response = new UserResponse
        {
            Token = accessToken, 
            Role = roles,
            RefreshToken = refreshToken
        };

        return new ServiceResponse<UserResponse>
        {
            Success = true,
            Data = response,
            Message = "Успешный вход",
        };
    }

    public async Task<User> CreateUserAsync(UserDTO userDto)
    {

        var user = new User
        {
            Name = userDto.Name,
            Surname = userDto.Surname,
            DateOfBirth = userDto.DateOfBirth,
            PasswordHash = userDto.PasswordHash,
            Login = userDto.Login,
            LastOnline = DateTime.UtcNow,
            CreationDate = DateTime.UtcNow
        };
        
        return await _repository.AddAsync(user);
    }
}