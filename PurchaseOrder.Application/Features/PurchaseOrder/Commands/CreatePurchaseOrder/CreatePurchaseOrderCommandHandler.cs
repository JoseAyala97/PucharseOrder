using MediatR;
using PurchaseOrder.Domain.Shared;

namespace PurchaseOrder.Application.Features.PurchaseOrder.Commands.CreatePurchaseOrder;

public class CreatePurchaseOrderCommandHandler : IRequestHandler<CreatePurchaseOrderCommand, GenericResponse>
{
    public async Task<GenericResponse> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
