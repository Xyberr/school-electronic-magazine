using school_electronic_magazine.DTO.Requests;

namespace school_electronic_magazine.Services;

public interface ISubjectService
{
    Task AddSubjectAsync(SubjectRequestPayload payload, CancellationToken cancellationToken);
    Task UpdateSubjectAsync(long subjectId, SubjectRequestPayload payload, CancellationToken cancellationToken);
    Task DeleteSubjectAsync(long subjectId, CancellationToken cancellationToken);
}