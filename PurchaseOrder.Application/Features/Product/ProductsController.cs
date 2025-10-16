using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchaseOrder.Application.Features.Product.Queries.GetAllProduct;
using PurchaseOrder.Domain.DTO;

namespace PurchaseOrder.Application.Features.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IMediator _Mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAll(CancellationToken ct)
        {
            var result = await _Mediator.Send(new GetAllProductsQuery(), ct);
            return Ok(result);
        }
    }
}
