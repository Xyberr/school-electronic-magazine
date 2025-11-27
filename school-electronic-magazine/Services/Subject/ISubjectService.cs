using school_electronic_magazine.DTO.Requests;

namespace school_electronic_magazine.Services.Subject;

public interface ISubjectService
{
    Task AddSubjectAsync(SubjectRequestPayload payload);
    Task UpdateSubjectAsync(long subjectId, SubjectRequestPayload payload);
    Task DeleteSubjectAsync(long subjectId);
}