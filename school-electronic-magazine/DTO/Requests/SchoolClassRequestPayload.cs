namespace school_electronic_magazine.DTO.Requests;

public record SchoolClassRequestPayload
{
    public required string GroupLabel { get; init; }

    public required int ClassNumber { get; init; }
    
    public long GroupId { get; init; }

    public required DateTime EnterDate { get; init; }
}