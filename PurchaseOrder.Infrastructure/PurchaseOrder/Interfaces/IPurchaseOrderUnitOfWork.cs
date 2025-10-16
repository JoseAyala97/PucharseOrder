﻿namespace PurchaseOrder.Infrastructure.PurchaseOrder.Interfaces;

public interface IPurchaseOrderUnitOfWork : IAsyncDisposable
{
    IPurchaseOrderRepository Repository { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
    Task BeginTransactionAsync(CancellationToken ct = default);
    Task CommitAsync(CancellationToken ct = default);
    Task RollbackAsync(CancellationToken ct = default);
}
