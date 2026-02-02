using school_electronic_magazine.DTO.Requests;

namespace school_electronic_magazine.Repositories;

public interface IUserRepository : IGenericRepository<Models.User>
{
    Task<Models.User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken);
}