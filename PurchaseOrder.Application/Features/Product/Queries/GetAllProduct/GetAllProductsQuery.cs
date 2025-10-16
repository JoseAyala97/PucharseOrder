using MediatR;
using PurchaseOrder.Domain.DTO;

namespace PurchaseOrder.Application.Features.Product.Queries.GetAllProduct;

public record GetAllProductsQuery : IRequest<IReadOnlyList<ProductDto>>;
