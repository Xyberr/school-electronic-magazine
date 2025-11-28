namespace school_electronic_magazine.Repositories.Group;

public interface IGroupRepository : IGenericRepository<Models.Group>
{
    Task<bool> ExistsAsync(long classId, long studentId);
}