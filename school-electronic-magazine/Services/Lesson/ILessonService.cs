using school_electronic_magazine.DTO.Requests;

namespace school_electronic_magazine.Services;

public interface ILessonService
{
    Task AddLessonAsync(LessonRequestPayload payload);
    Task UpdateLessonAsync(long lessonId, LessonRequestPayload payload);
    Task DeleteLessonAsync(long lessonId);
}