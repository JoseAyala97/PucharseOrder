namespace PurchaseOrder.Domain.Entities;

public class State
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<PuchaseOrder> PurchaseOrders { get; set; }
}
