namespace school_electronic_magazine.DTO.Requests;

public record GradeRequestPayload
{
    public long StudentId { get; init; }
    
    public long SchoolClassId { get; set; }
    
    public long LessonId { get; set; }
    
    public string Value { get; set; }
}