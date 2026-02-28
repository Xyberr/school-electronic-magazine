namespace school_electronic_magazine.DTO.Requests;

public record SubjectRequestPayload
{
    public required string Name { get; init; } = null!;
}