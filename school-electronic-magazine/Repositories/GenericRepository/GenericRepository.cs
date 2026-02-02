using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.Data;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Models;

namespace school_electronic_magazine.Repositories;

public class GenericRepository<T> : IGenericRepository<T>
    where T : class
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<T> DbSet;

    public GenericRepository(AppDbContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        DbSet = Context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await DbSet.FindAsync(
            new object[] { id },
            cancellationToken
        );
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbSet
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        await DbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public void Update(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        DbSet.Update(entity);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return;

        DbSet.Remove(entity);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await Context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<T> Query()
    {
        return DbSet.AsQueryable();
    }
}