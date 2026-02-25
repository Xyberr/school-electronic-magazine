namespace school_electronic_magazine.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken, bool asNoTracking = true);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);

    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    void Update(T entity);
    Task<int> DeleteByIdAsync(long id, CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    IQueryable<T> Query();
}