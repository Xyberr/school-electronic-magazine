namespace school_electronic_magazine.DTO.Requests;

public class GradeRequestPayload
{
    public long StudentId { get; set; }
    
    public long SchoolClassId { get; set; }
    
    public long LessonId { get; set; }
    
    public string Value { get; set; }
}