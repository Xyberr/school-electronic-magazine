using Microsoft.AspNetCore.Identity.Data;

namespace school_electronic_magazine.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(long id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(long id);
    Task<int> SaveChangesAsync();
    IQueryable<T> Query();
}