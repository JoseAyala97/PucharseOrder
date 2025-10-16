using MediatR;
using PurchaseOrder.Domain.DTO;
using PurchaseOrder.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace PurchaseOrder.Application.Features.PurchaseOrder.Commands.CreatePurchaseOrder;

public record CreatePurchaseOrderItemCmd(
        [Required] int ProductId,
        [Range(1, int.MaxValue)] int Quantity
    );

public class CreatePurchaseOrderCommand : IRequest<GenericResponse>
{
    [Required] public int CustomerId { get; init; }
    [Required, MinLength(3)] public string DeliveryAddress { get; init; } = default!;
    [Required] public List<CreatePurchaseOrderItemCmd> Items { get; init; } = new();
}
