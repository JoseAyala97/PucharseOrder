using AutoMapper;
using MediatR;
using PurchaseOrder.Domain.DTO;
using PurchaseOrder.Infrastructure.PurchaseOrder.Interfaces;
using DomainProduct = PurchaseOrder.Domain.Entities.Product;

namespace PurchaseOrder.Application.Features.Product.Queries.GetAllProduct;

public class GetAllProductsQueryHandler(IPurchaseOrderRepository _repo,
    IMapper _mapper) : IRequestHandler<GetAllProductsQuery, IReadOnlyList<ProductDto>>
{
    public async Task<IReadOnlyList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var Products = await _repo.ListAsync<DomainProduct>(
            predicate: null,
            asNoTracking: true,
            ct: cancellationToken);

        var dtos = _mapper.Map<IReadOnlyList<ProductDto>>(Products);
        return dtos;
    }
}
