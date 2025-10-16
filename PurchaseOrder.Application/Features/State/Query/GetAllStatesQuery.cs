using MediatR;
using PurchaseOrder.Domain.DTO;

namespace PurchaseOrder.Application.Features.State.Query;

public record GetAllStatesQuery : IRequest<IReadOnlyList<StateDto>>;

