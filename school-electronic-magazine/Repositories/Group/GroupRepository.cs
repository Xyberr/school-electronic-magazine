using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.Data;

namespace school_electronic_magazine.Repositories.Group;


public class GroupRepository : GenericRepository<Models.Group>, IGroupRepository
{
    private readonly AppDbContext _context;

    public GroupRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(long classId, long studentId)
    {
        return await _context.Set<Models.Group>()
            .AnyAsync(g => g.ClassId == classId && g.StudentId == studentId);
    }
}