using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.Data;

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

    public async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken, bool asNoTracking = true)
    {
        IQueryable<T> query = DbSet;

        if (asNoTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(
            e => EF.Property<long>(e, "Id") == id,
            cancellationToken);
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

    public async Task<int> DeleteByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(e => EF.Property<long>(e, "Id") == id)
            .ExecuteDeleteAsync(cancellationToken);
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await Context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<T> Query()
    {
        return DbSet.AsNoTracking();
    }
}