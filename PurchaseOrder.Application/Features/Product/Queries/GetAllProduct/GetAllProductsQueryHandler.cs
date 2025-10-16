using AutoMapper;
using MediatR;
using PurchaseOrder.Domain.DTO;
using PurchaseOrder.Infrastructure.PurchaseOrder.Interfaces;

namespace PurchaseOrder.Application.Features.Product.Queries.GetAllProduct;

public class GetAllProductsQueryHandler(IPurchaseOrderRepository _repo,
    IMapper _mapper) : IRequestHandler<GetAllProductsQuery, IReadOnlyList<ProductDto>>
{
    public async Task<IReadOnlyList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var Products = await _repo.ListAsync<PurchaseOrder.Domain.Entities.Product>(
            predicate: null,
            asNoTracking: true,
            ct: cancellationToken);

        var dtos = _mapper.Map<IReadOnlyList<ProductDto>>(Products);
        return dtos;
    }
}
