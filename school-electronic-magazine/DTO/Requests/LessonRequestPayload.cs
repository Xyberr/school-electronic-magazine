namespace school_electronic_magazine.DTO.Requests;

public record LessonRequestPayload
{
    public long SubjectId { get; init; }
    
    public long TeacherId { get; init; }
    
    public required DateTime LessonDate { get; init; }
    
    public required string ClassRoom { get; init; } = null!;
    
    public required string Title { get; init; } = null!;
}