using Microsoft.EntityFrameworkCore.Storage;
using PurchaseOrder.Infrastructure.PurchaseOrder.Interfaces;

namespace PurchaseOrder.Infrastructure.PurchaseOrder.Repository;

public class PurchaseOrderUnitOfWork : IPurchaseOrderUnitOfWork
{
    private readonly PurchaseOrderContext _db;
    private readonly IPurchaseOrderRepository _repository;
    private IDbContextTransaction? _tx;

    public PurchaseOrderUnitOfWork(PurchaseOrderContext db, IPurchaseOrderRepository repository)
    {
        _db = db;
        _repository = repository;
    }

    public IPurchaseOrderRepository Repository => _repository;

    public async Task BeginTransactionAsync(CancellationToken ct = default)
    {
        if (_tx is not null) return; 
        _tx = await _db.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitAsync(CancellationToken ct = default)
    {
        if (_tx is null) return;
        await _db.SaveChangesAsync(ct); 
        await _tx.CommitAsync(ct);
        await _tx.DisposeAsync();
        _tx = null;
    }

    public async Task RollbackAsync(CancellationToken ct = default)
    {
        if (_tx is null) return;
        await _tx.RollbackAsync(ct);
        await _tx.DisposeAsync();
        _tx = null;
    }

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);

    public async ValueTask DisposeAsync()
    {
        if (_tx is not null)
        {
            await _tx.DisposeAsync();
            _tx = null;
        }
    }
}
