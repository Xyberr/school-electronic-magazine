using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.Data;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Models;

namespace school_electronic_magazine.Repositories.Users;

public class UserRepository(AppDbContext context) : GenericRepository<User>(context), IUserRepository
{
    private readonly DbSet<User> _dbSet = context.Set<User>();

    public Task<User?> GetUserByLoginAsync(string login)
        => _dbSet.Include(user => user.Roles)
            .FirstOrDefaultAsync(user => user.Login == login);
}