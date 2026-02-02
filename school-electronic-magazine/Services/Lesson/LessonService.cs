using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;

namespace school_electronic_magazine.Services;

public class LessonService(IGenericRepository<Lesson> lessonRepository, IGenericRepository<User> userRepository, IGenericRepository<Subject> subjectRepository) : ILessonService
{
    public async Task AddLessonAsync(LessonRequestPayload payload, CancellationToken cancellationToken)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        {
            var teacherExists = await userRepository.Query()
                .AnyAsync(t => t.Id == payload.TeacherId);
            if (!teacherExists)
                throw new InvalidOperationException("Учитель не найден");
        }

        {
            var subjectExists = await subjectRepository.Query()
                .AnyAsync(s => s.Id == payload.SubjectId);
            if (!subjectExists)
                throw new InvalidOperationException("Предмет не найден");
        }

        var lesson = new Lesson
        {
            LessonDate = payload.LessonDate,
            ClassRoom = payload.ClassRoom,
            Title = payload.Title,
            SubjectId = payload.SubjectId,
            CreationDate = DateTime.UtcNow,
            ModificationDate = DateTime.UtcNow,
        };

        await lessonRepository.AddAsync(lesson, cancellationToken);
        await lessonRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateLessonAsync(long lessonId, LessonRequestPayload payload, CancellationToken cancellationToken)
    {
       var lesson = await lessonRepository.GetByIdAsync(lessonId, cancellationToken);
       
       if(lesson == null)
           throw new InvalidOperationException("Урок не найден");
       
       lesson.Title = payload.Title;
       lesson.ClassRoom = payload.ClassRoom;
       lesson.LessonDate = payload.LessonDate;
        
       lessonRepository.Update(lesson);
       await lessonRepository.SaveChangesAsync(cancellationToken);
       
    }

    //Нужно по ТЗ
    public async Task DeleteLessonAsync(long lessonId, CancellationToken cancellationToken)
    {
        var lesson = await lessonRepository.GetByIdAsync(lessonId, cancellationToken);
        
        if (lesson == null)
            throw new InvalidOperationException("Урок не найден");
        
        await lessonRepository.DeleteAsync(lessonId, cancellationToken);
        await lessonRepository.SaveChangesAsync(cancellationToken);
    }
}