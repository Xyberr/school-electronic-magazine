namespace school_electronic_magazine.DTO.Requests;

public class LessonRequestPayload
{
    public long SubjectId { get; set; }
    
    public long TeacherId { get; set; }
    
    public required DateTime LessonDate { get; set; }
    
    public required string ClassRoom { get; set; } = null!;
    
    public required string Title { get; set; } = null!;
}