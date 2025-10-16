using PurchaseOrder.Domain.Entities;

namespace PurchaseOrder.Domain.DTO;

public class PuchaseOrderDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public CustomerDto Customer { get; set; }
    public string DeliveryAddress { get; set; }
    public int StateId { get; set; }
    public State State { get; set; }
    public int PriorityId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public decimal TotalAmount { get; set; }
}
