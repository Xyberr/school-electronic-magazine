using school_electronic_magazine.DTO.Requests;

namespace school_electronic_magazine.Services;

public interface IGradeService
{
    Task AddGradeAsync(GradeRequestPayload payload);
    Task UpdateGradeAsync(long gradeId, GradeRequestPayload payload);
    Task DeleteGradeAsync(long gradeId);
}