using Microsoft.EntityFrameworkCore;
using PurchaseOrder.Infrastructure.PurchaseOrder.Interfaces;
using System.Linq.Expressions;

namespace PurchaseOrder.Infrastructure.PurchaseOrder.Repository;

public class PurchaseOrderRepository : IPurchaseOrderRepository
{
        private readonly PurchaseOrderContext _db;

    public PurchaseOrderRepository(PurchaseOrderContext db) => _db = db;

    public async Task<T?> GetByIdAsync<T>(CancellationToken ct = default, params object[] keyValues) where T : class
        => await _db.Set<T>().FindAsync(keyValues, ct);

    public async Task<IReadOnlyList<T>> ListAsync<T>(
        Expression<Func<T, bool>>? predicate = null,
        bool asNoTracking = true,
        CancellationToken ct = default) where T : class
    {
        IQueryable<T> query = _db.Set<T>();
        if (asNoTracking) query = query.AsNoTracking();
        if (predicate is not null) query = query.Where(predicate);
        return await query.ToListAsync(ct);
    }

    public Task AddAsync<T>(T entity, CancellationToken ct = default) where T : class
        => _db.Set<T>().AddAsync(entity, ct).AsTask();

    public Task AddRangeAsync<T>(IEnumerable<T> entities, CancellationToken ct = default) where T : class
        => _db.Set<T>().AddRangeAsync(entities, ct);

    public void Update<T>(T entity) where T : class
        => _db.Set<T>().Update(entity);

    public void Remove<T>(T entity) where T : class
        => _db.Set<T>().Remove(entity);

    public void RemoveRange<T>(IEnumerable<T> entities) where T : class
        => _db.Set<T>().RemoveRange(entities);

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);
}
