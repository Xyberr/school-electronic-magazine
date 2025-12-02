using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Repositories;

namespace school_electronic_magazine.Services.Lesson;

public class LessonService(IGenericRepository<Models.Lesson> genericRepository) : ILessonService
{
    public async Task AddLessonAsync(LessonRequestPayload payload)
    {
        Models.Lesson lesson = new Models.Lesson
        {
            LessonDate = payload.LessonDate,
            ClassRoom = payload.ClassRoom,
            Title = payload.Title,
            CreationDate = DateTime.Now
        };

        await genericRepository.AddAsync(lesson);
        await genericRepository.SaveChangesAsync();
    }

    public async Task UpdateLessonAsync(long lessonId, LessonRequestPayload payload)
    {
       var lesson = await genericRepository.GetByIdAsync(lessonId);
       
       if(lesson == null)
           throw new InvalidOperationException("Урок не найден");
       
       lesson.Title = payload.Title;
       lesson.ClassRoom = payload.ClassRoom;
       lesson.LessonDate = payload.LessonDate;
        
       await genericRepository.UpdateAsync(lesson);
       
    }

    public async Task DeleteLessonAsync(long lessonId)
    {
        var lesson = await genericRepository.GetByIdAsync(lessonId);
        
        await genericRepository.DeleteAsync(lessonId);
        await genericRepository.SaveChangesAsync();
    }
}