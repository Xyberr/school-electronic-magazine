namespace school_electronic_magazine.Repositories;

public interface ISubjectRepository : IGenericRepository<Models.Subject>
{
    Task<Models.Subject?> GetByNameAsync(string name, CancellationToken cancellationToken);
}