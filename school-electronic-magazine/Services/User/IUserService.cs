using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.DTO.Responses;
using school_electronic_magazine.Models;

namespace school_electronic_magazine.Services;

public interface IUserService
{
    Task<UserAuthResponsePayload> AuthorizeUserAsync(UserAuthRequestPayload userAuthRequestPayload, CancellationToken cancellationToken);
    Task<User> CreateUserAsync(UserRegisterRequestPayload userDto, CancellationToken cancellationToken);
    Task AssignRolesAsync(long userId, List<string> roles, CancellationToken cancellationToken);
    Task RemoveRolesAsync(long userId, List<string> roles, CancellationToken cancellationToken);
    Task RemoveUserByIdAsync(long userId, CancellationToken cancellationToken);
}