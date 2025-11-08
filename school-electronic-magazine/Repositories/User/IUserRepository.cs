using school_electronic_magazine.DTO.Requests;

namespace school_electronic_magazine.Repositories.User;

public interface IUserRepository : IGenericRepository<Models.User>
{
    Task<Models.User?> GetUserByLoginAndPasswordAsync(UserAuthLoginRequest userAuthLoginRequest);
}