namespace AnalyticsService.DataAccess.Abstractions;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

    Task AddAsync(T entity, CancellationToken cancellationToken);
    void Update(T entity, CancellationToken cancellationToken);
    void Remove(T entity, CancellationToken cancellationToken);
}