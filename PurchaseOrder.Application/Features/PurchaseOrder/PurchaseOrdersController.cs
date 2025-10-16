using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchaseOrder.Application.Features.PurchaseOrder.Commands.CreatePurchaseOrder;
using PurchaseOrder.Domain.Shared;

namespace PurchaseOrder.Application.Features.PurchaseOrder
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrdersController(IMediator _mediator) : ControllerBase
    {
        // <summary>
        /// Crea una nueva orden de compra.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(GenericResponse), 200)]
        public async Task<ActionResult<GenericResponse>> Create(
            [FromBody] CreatePurchaseOrderCommand command,
            CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }
    }
}
