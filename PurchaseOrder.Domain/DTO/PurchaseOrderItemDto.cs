namespace PurchaseOrder.Domain.DTO;

public class PurchaseOrderItemDto
{
    public int Id { get; set; }
    public int PurchaseOrderId { get; set; }
    public PuchaseOrderDto PucharseOrder { get; set; }
    public int ProductId { get; set; }
    public ProductDto Product { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal { get; set; }
}
