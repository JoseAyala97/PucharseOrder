using MediatR;
using PurchaseOrder.Domain.DTO;

namespace PurchaseOrder.Application.Features.Customer.Queries.GetAllCustomers
{
    public record GetAllCustomersQuery : IRequest<IReadOnlyList<CustomerDto>>;
}
