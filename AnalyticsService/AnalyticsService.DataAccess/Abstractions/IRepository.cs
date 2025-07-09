namespace AnalyticsService.DataAccess.Abstractions;

public interface IRepository<T, TId> where T : class
{
    Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
    void Update(T entity, CancellationToken cancellationToken);
    void Remove(T entity, CancellationToken cancellationToken);
}