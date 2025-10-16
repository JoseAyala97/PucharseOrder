using AutoMapper;
using MediatR;
using PurchaseOrder.Domain.DTO;
using PurchaseOrder.Infrastructure.PurchaseOrder.Interfaces;

namespace PurchaseOrder.Application.Features.State.Query;

public class GetAllStatesQueryHandler(IPurchaseOrderRepository _repo,
    IMapper _mapper)
    : IRequestHandler<GetAllStatesQuery, IReadOnlyList<StateDto>>
{
    public async Task<IReadOnlyList<StateDto>> Handle(GetAllStatesQuery request, CancellationToken cancellationToken)
    {
        var States = await _repo.ListAsync<PurchaseOrder.Domain.Entities.State>(
            predicate: null,
            asNoTracking: true,
            ct: cancellationToken);

        var dtos = _mapper.Map<IReadOnlyList<StateDto>>(States);
        return dtos;
    }
}
