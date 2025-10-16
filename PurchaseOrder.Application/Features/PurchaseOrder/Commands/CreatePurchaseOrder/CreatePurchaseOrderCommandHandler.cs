using MediatR;
using PurchaseOrder.Domain.Shared;
using PurchaseOrder.Infrastructure.PurchaseOrder.Interfaces;
using DomainCustomer = PurchaseOrder.Domain.Entities.Customer;
using DomainPO = PurchaseOrder.Domain.Entities.PuchaseOrder;
using DomainPOItem = PurchaseOrder.Domain.Entities.PurchaseOrderItem;
using DomainProduct = PurchaseOrder.Domain.Entities.Product;

namespace PurchaseOrder.Application.Features.PurchaseOrder.Commands.CreatePurchaseOrder;

public class CreatePurchaseOrderCommandHandler(
    IPurchaseOrderRepository _repo,
    IPurchaseOrderUnitOfWork _uow
) : IRequestHandler<CreatePurchaseOrderCommand, GenericResponse>
{
    public async Task<GenericResponse> Handle(CreatePurchaseOrderCommand request, CancellationToken ct)
    {
        if (request.Items is null || request.Items.Count == 0)
            return GenericResponse.Fail("Debe incluir al menos un producto.");

        if (string.IsNullOrWhiteSpace(request.DeliveryAddress))
            return GenericResponse.Fail("La dirección de entrega es obligatoria.");

        var customer = await _repo.GetByIdAsync<DomainCustomer>(ct, request.CustomerId);
        if (customer is null) return GenericResponse.Fail("El cliente no existe.");

        var productIds = request.Items.Select(i => i.ProductId).Distinct().ToList();
        var products = await _repo.ListAsync<DomainProduct>(
            p => productIds.Contains(p.Id),
            asNoTracking: true,
            ct: ct);

        if (products.Count != productIds.Count)
            return GenericResponse.Fail("Alguno(s) de los productos no existe(n).");

        var order = new DomainPO
        {
            CustomerId = request.CustomerId,
            DeliveryAddress = request.DeliveryAddress,
            StateId = 1, 
            CreatedDate = DateTime.UtcNow,
            PurchaseOrderItems = new List<DomainPOItem>()
        };

        foreach (var li in request.Items)
        {
            if (li.Quantity <= 0)
                return GenericResponse.Fail("La cantidad debe ser mayor a cero.");

            var p = products.First(x => x.Id == li.ProductId);

            order.PurchaseOrderItems.Add(new DomainPOItem
            {
                ProductId = p.Id,
                UnitPrice = p.Price,
                Quantity = li.Quantity
            });
        }

        order.TotalAmount = order.PurchaseOrderItems.Sum(x => x.UnitPrice * x.Quantity);
        order.PriorityId = ResolvePriority(order.TotalAmount); 

        await _uow.BeginTransactionAsync(ct);
        try
        {
            await _repo.AddAsync(order, ct);
            await _uow.SaveChangesAsync(ct); 
            await _uow.CommitAsync(ct);
        }
        catch
        {
            await _uow.RollbackAsync(ct);
            throw;
        }

        return GenericResponse.Ok(
            data: new { order.Id, order.TotalAmount, order.PriorityId },
            message: "Orden creada correctamente.",
            id: order.Id
        );
    }

    private static int ResolvePriority(decimal total)
    {
        if (total <= 500m) return 1;      
        if (total <= 1000m) return 2;     
        return 3;                         
    }
}
