namespace school_electronic_magazine.DTO.Response;

public class ServiceResponse<T>
{
    public required bool Success { get; set; }
    public required string Message { get; set; } = "";
    public T? Data { get; set; }      
}