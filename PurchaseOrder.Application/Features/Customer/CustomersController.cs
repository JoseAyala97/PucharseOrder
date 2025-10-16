using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchaseOrder.Application.Features.Customer.Queries.GetAllCustomers;
using PurchaseOrder.Domain.DTO;

namespace PurchaseOrder.Application.Features.Customer
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController(IMediator _Mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CustomerDto>>> GetAll(CancellationToken ct)
        {
            var result = await _Mediator.Send(new GetAllCustomersQuery(), ct);
            return Ok(result);
        }
    }
}
