using school_electronic_magazine.DTO.Requests;

namespace school_electronic_magazine.Services;

public interface IGradeService
{
    Task AddGradeAsync(GradeRequestPayload payload, CancellationToken cancellationToken);
    Task UpdateGradeAsync(long gradeId, GradeRequestPayload payload, CancellationToken cancellationToken);
    Task DeleteGradeAsync(long gradeId, CancellationToken cancellationToken);
}