namespace PurchaseOrder.Domain.Entities;

public class PuchaseOrder
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public string DeliveryAddress { get; set; }
    public int StateId { get; set; }
    public virtual State State { get; set; }
    public int PriorityId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public decimal TotalAmount { get; set; }
    public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
}
