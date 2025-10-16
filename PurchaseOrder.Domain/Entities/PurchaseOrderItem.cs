namespace PurchaseOrder.Domain.Entities;

public class PurchaseOrderItem
{
    public int Id { get; set; }
    public int PurchaseOrderId { get; set; }
    public virtual PuchaseOrder PucharseOrder { get; set; }
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal { get; set; }
}
