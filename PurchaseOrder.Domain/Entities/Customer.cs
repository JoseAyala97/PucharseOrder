namespace PurchaseOrder.Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Identification { get; set; }
    public ICollection<PuchaseOrder> PurchaseOrders { get; set; }
}
