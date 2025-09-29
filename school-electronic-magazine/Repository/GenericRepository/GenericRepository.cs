using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.Data;
using school_electronic_magazine.DTO;
using school_electronic_magazine.Models;

namespace school_electronic_magazine.Repository.GenericRepository;

public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>(); // Для работы с отдельной таблицей

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(long id, T entity)
    {
        var dbEntity = await _dbSet.FindAsync(id);
        if (dbEntity != null) _dbSet.Update(dbEntity);
        await context.SaveChangesAsync(); 
        return entity;
    }

    public async Task<long?> DeleteAsync(long id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) return null;
        _dbSet.Remove(entity);
        await context.SaveChangesAsync();   
        return id;
    }
}