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

        var teacherExists = await userRepository.Query()
            .AnyAsync(t => t.Id == payload.TeacherId, cancellationToken);
        if (!teacherExists)
            throw new InvalidOperationException("Учитель не найден");

        var subjectExists = await subjectRepository.Query()
            .AnyAsync(s => s.Id == payload.SubjectId, cancellationToken);
        if (!subjectExists)
            throw new InvalidOperationException("Предмет не найден");
        
        var now = DateTime.UtcNow;

        var lesson = new Lesson
        {
            LessonDate = payload.LessonDate,
            SubjectId = payload.SubjectId,
            ClassRoom = payload.ClassRoom,
            Title = payload.Title,
            CreationDate = now,
            ModificationDate = now,
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
       lesson.SubjectId = payload.SubjectId;
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
        
        await lessonRepository.DeleteByIdAsync(lessonId, cancellationToken);
        await lessonRepository.SaveChangesAsync(cancellationToken);
    }
}