using Microsoft.AspNetCore.Identity.Data;
using school_electronic_magazine.DTO;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.DTO.Response;
using school_electronic_magazine.Models;
namespace school_electronic_magazine.Services.Auth;

public interface IUserService
{
    Task<ServiceResponse<UserResponse>> GetUserByLoginAndPasswordAsync(UserAuthLoginRequest userAuthLoginRequest);
    Task<User> CreateUserAsync(UserDTO userDto);
}