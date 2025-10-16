using AutoMapper;
using MediatR;
using PurchaseOrder.Domain.DTO;
using PurchaseOrder.Infrastructure.PurchaseOrder.Interfaces;
using DomainCustomer = PurchaseOrder.Domain.Entities.Customer;

namespace PurchaseOrder.Application.Features.Customer.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler(IPurchaseOrderRepository _repo,
    IMapper _mapper)
       : IRequestHandler<GetAllCustomersQuery, IReadOnlyList<CustomerDto>>
{
    public async Task<IReadOnlyList<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _repo.ListAsync<DomainCustomer>(
            predicate: null,
            asNoTracking: true,
            ct: cancellationToken);

        var dtos = _mapper.Map<IReadOnlyList<CustomerDto>>(customers);
        return dtos;
    }
}
