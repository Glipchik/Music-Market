using AnalyticsService.DataAccess.Abstractions;
using AnalyticsService.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.DataAccess.Repositories;

public class Repository<T>(ApplicationDbContext context) : IRepository<T>
    where T : class
{
    protected readonly DbSet<T> DbSet = context.Set<T>();

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbSet.FindAsync([id, cancellationToken], cancellationToken);
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }

    public void Update(T entity, CancellationToken cancellationToken)
    {
        DbSet.Update(entity);
    }

    public void Remove(T entity, CancellationToken cancellationToken)
    {
        DbSet.Remove(entity);
    }
}