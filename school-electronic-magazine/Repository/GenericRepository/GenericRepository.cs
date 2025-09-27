using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.Data;
using school_electronic_magazine.Models;

namespace school_electronic_magazine.Repository.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context; // Для работы со всей БД
    private readonly DbSet<T> _dbSet; // Для работы с отдельной таблицей
    
    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> CreateAsync(T Entity)
    {
        await _context.Set<T>().AddAsync(Entity);
        await _context.SaveChangesAsync();
        return Entity;
    }

    public async Task<T> UpdateAsync(int id, T Entity)
    {
        _context.Set<T>().Update(Entity);
        await _context.SaveChangesAsync();
        return Entity;
    }

    public async Task<T?> DeleteAsync(int id)
    {
        _context.Set<T>().Remove(await _context.Set<T>().FindAsync(id));
        await _context.SaveChangesAsync();
        
        return null;
    }
}