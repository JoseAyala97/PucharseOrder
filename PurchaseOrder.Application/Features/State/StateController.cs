using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchaseOrder.Application.Features.State.Query;
using PurchaseOrder.Domain.DTO;

namespace PurchaseOrder.Application.Features.State;

[ApiController]
[Route("api/[controller]")]
public class StateController(IMediator _Mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<StateDto>>> GetAll(CancellationToken ct)
    {
        var result = await _Mediator.Send(new GetAllStatesQuery(), ct);
        return Ok(result);
    }
}
