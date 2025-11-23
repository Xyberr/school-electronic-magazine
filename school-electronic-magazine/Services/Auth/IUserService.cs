using Microsoft.AspNetCore.Identity.Data;
using school_electronic_magazine.DTO;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.DTO.Response;
using school_electronic_magazine.Models;
namespace school_electronic_magazine.Services.Auth;

public interface IUserService
{
    Task<UserAuthResponcePayload> AuthorizeUserAsync(UserAuthRequestPayload userAuthRequestPayload);
    Task<User> CreateUserAsync(UserRegisterRequestPayload userDto);
    Task AddRolesAsync(long userId, List<string> roles);
    Task RemoveRolesAsync(long userId, List<string> roles);
}