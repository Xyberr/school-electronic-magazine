using school_electronic_magazine.Data;

namespace school_electronic_magazine.Repositories.RefreshToken;

public class RefreshTokenRepository : GenericRepository<Models.RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AppDbContext context) : base(context)
    {
        
    }
}