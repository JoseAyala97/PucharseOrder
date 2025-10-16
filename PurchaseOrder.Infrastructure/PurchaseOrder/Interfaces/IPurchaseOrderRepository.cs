using System.Linq.Expressions;

namespace PurchaseOrder.Infrastructure.PurchaseOrder.Interfaces;

public interface IPurchaseOrderRepository
{
    Task<T?> GetByIdAsync<T>(CancellationToken ct = default, params object[] keyValues) where T : class;

    Task<IReadOnlyList<T>> ListAsync<T>(
        Expression<Func<T, bool>>? predicate = null,
        bool asNoTracking = true,
        CancellationToken ct = default) where T : class;

    Task AddAsync<T>(T entity, CancellationToken ct = default) where T : class;
    Task AddRangeAsync<T>(IEnumerable<T> entities, CancellationToken ct = default) where T : class;

    void Update<T>(T entity) where T : class;
    void Remove<T>(T entity) where T : class;
    void RemoveRange<T>(IEnumerable<T> entities) where T : class;

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
