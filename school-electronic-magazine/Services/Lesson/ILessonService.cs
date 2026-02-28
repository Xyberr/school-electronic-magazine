using school_electronic_magazine.DTO.Requests;

namespace school_electronic_magazine.Services;

public interface ILessonService
{
    Task AddLessonAsync(LessonRequestPayload payload, CancellationToken cancellationToken);
    Task UpdateLessonAsync(long lessonId, LessonRequestPayload payload, CancellationToken cancellationToken);
    Task DeleteLessonAsync(long lessonId, CancellationToken cancellationToken);
}