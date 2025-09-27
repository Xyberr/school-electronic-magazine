namespace school_electronic_magazine.Repository.GenericRepository;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> CreateAsync(T Entity);
    Task<T> UpdateAsync(int id,T Entity);
    Task<T?> DeleteAsync(int id);
}