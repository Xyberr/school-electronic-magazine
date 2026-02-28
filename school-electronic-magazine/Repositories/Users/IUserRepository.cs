using school_electronic_magazine.Models;

namespace school_electronic_magazine.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken);
}