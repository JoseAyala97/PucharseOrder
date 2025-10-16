namespace PurchaseOrder.Domain.Shared;

public class GenericResponse
{
    public int Id { get; set; }
    public string Message { get; set; }
    public Object ObjectResponse { get; set; }
    public string Code { get; set; }
}
