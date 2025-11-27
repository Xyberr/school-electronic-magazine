namespace school_electronic_magazine.Repositories.Subject;

public interface ISubjectRepository : IGenericRepository<Models.Subject>
{
    Task<Models.Subject?> GetByNameAsync(string name);
}