namespace PurchaseOrder.Domain.Shared;

public class GenericResponse
{
    public int Id { get; set; }                      
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public object? ObjectResponse { get; set; }      
    public string? Code { get; set; }                

    public static GenericResponse Ok(object? data = null, string message = "OK", int id = 0, string? code = null)
        => new GenericResponse
        {
            Success = true,
            Message = message,
            ObjectResponse = data,
            Id = id,
            Code = code
        };

    public static GenericResponse Fail(string message, string? code = null, object? data = null)
        => new GenericResponse
        {
            Success = false,
            Message = message,
            ObjectResponse = data,
            Code = code
        };
}
