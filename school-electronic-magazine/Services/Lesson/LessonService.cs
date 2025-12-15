using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;

namespace school_electronic_magazine.Services;

public class LessonService(IGenericRepository<Lesson> lessonRepository, IGenericRepository<User> userRepository, IGenericRepository<Subject> subjectRepository) : ILessonService
{
    public async Task AddLessonAsync(LessonRequestPayload payload)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        if (payload.TeacherId.HasValue)
        {
            var teacherExists = await userRepository.Query()
                .AnyAsync(t => t.Id == payload.TeacherId.Value);
            if (!teacherExists)
                throw new InvalidOperationException("Учитель не найден");
        }

        if (payload.SubjectId.HasValue)
        {
            var subjectExists = await subjectRepository.Query()
                .AnyAsync(s => s.Id == payload.SubjectId.Value);
            if (!subjectExists)
                throw new InvalidOperationException("Предмет не найден");
        }

        var lesson = new Lesson
        {
            LessonDate = payload.LessonDate,
            ClassRoom = payload.ClassRoom,
            Title = payload.Title,
            TeacherId = payload.TeacherId ?? 0,
            SubjectId = payload.SubjectId ?? 0,
            CreationDate = DateTime.UtcNow,
        };

        await lessonRepository.AddAsync(lesson);
        await lessonRepository.SaveChangesAsync();
    }

    public async Task UpdateLessonAsync(long lessonId, LessonRequestPayload payload)
    {
       var lesson = await lessonRepository.GetByIdAsync(lessonId);
       
       if(lesson == null)
           throw new InvalidOperationException("Урок не найден");
       
       lesson.Title = payload.Title;
       lesson.ClassRoom = payload.ClassRoom;
       lesson.LessonDate = payload.LessonDate;
        
       await lessonRepository.UpdateAsync(lesson);
       await lessonRepository.SaveChangesAsync();
       
    }

    //Тут так же, если нужно могу убрать, но по ТЗ должен быть CRUD
    public async Task DeleteLessonAsync(long lessonId)
    {
        var lesson = await lessonRepository.GetByIdAsync(lessonId);
        
        await lessonRepository.DeleteAsync(lessonId);
        await lessonRepository.SaveChangesAsync();
    }
}