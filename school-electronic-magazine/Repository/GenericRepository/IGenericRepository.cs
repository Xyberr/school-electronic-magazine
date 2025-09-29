using school_electronic_magazine.DTO;

namespace school_electronic_magazine.Repository.GenericRepository;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(long id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(long id,T entity);
    Task<long?> DeleteAsync(long id);
}