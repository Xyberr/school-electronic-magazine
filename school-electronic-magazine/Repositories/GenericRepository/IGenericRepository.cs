using Microsoft.AspNetCore.Identity.Data;

namespace school_electronic_magazine.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);

    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    void Update(T entity);
    Task DeleteAsync(long id, CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    IQueryable<T> Query();
}