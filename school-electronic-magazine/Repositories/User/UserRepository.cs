using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.Data;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Services.Auth;

namespace school_electronic_magazine.Repositories.User;

public class UserRepository : GenericRepository<Models.User>, IUserRepository
{
    
    private readonly AppDbContext _context;
    private readonly DbSet<Models.User> _dbSet;


    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Models.User>();
    }


    public async Task<Models.User?> GetUserByLoginAndPasswordAsync(UserAuthLoginRequest userAuthLoginRequest)
    {
        return await _dbSet
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u =>
                u.Login == userAuthLoginRequest.Login &&
                u.PasswordHash == userAuthLoginRequest.Password);
    }
}