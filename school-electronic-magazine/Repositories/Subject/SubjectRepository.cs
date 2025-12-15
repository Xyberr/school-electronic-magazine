using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.Data;

namespace school_electronic_magazine.Repositories;

public class SubjectRepository : GenericRepository<Models.Subject>, ISubjectRepository
{
    private readonly AppDbContext _context;

    public SubjectRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Models.Subject?> GetByNameAsync(string name)
    {
        return await _context.Set<Models.Subject>()
            .FirstOrDefaultAsync(s => s.Name == name);
    }
}