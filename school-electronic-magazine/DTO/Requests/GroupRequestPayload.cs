namespace school_electronic_magazine.DTO.Requests;

public record GroupRequestPayload
{
    public long? ClassId { get; init; }
    
    public long StudentId { get; init; }
}