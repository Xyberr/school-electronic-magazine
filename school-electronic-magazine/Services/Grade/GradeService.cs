using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;

namespace school_electronic_magazine.Services;

public class GradeService(IGenericRepository<Grade> repository, IGenericRepository<Student> studentRepository, IGenericRepository<Lesson> lessonRepository) : IGradeService
{
    public async Task AddGradeAsync(GradeRequestPayload payload,CancellationToken cancellationToken)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        cancellationToken.ThrowIfCancellationRequested();
        
        var gradeExists = await repository.Query()
            .AnyAsync(g =>
                g.StudentId == payload.StudentId &&
                g.LessonId == payload.LessonId);

        if (gradeExists)
            throw new InvalidOperationException("Оценка за этот урок уже проставлена");

        var studentExists = await studentRepository.Query()
            .AnyAsync(s => s.Id == payload.StudentId);

        if (!studentExists)
            throw new InvalidOperationException("Студент не найден");

        var lessonExists = await lessonRepository.Query()
            .AnyAsync(l => l.Id == payload.LessonId);

        if (!lessonExists)
            throw new InvalidOperationException("Урок не найден");

        var grade = new Grade
        {
            StudentId = payload.StudentId,
            LessonId = payload.LessonId,
            Value = payload.Value,
            CreationDate = DateTime.UtcNow,
            ModificationDate = DateTime.UtcNow
        };

        await repository.AddAsync(grade, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateGradeAsync(long gradeId, GradeRequestPayload payload, CancellationToken cancellationToken)
    {
        if (payload == null)
            throw new ArgumentNullException("Данные пустые");
        
        var grade = await repository.GetByIdAsync(gradeId, cancellationToken);
        
        grade.Value = payload.Value;
        grade.StudentId = payload.StudentId;
        grade.LessonId = payload.LessonId;

        repository.Update(grade);
        await repository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteGradeAsync(long gradeId, CancellationToken cancellationToken)
    {
        if (gradeId == null)
            throw new ArgumentNullException("id не может быть null");
        
        await repository.DeleteAsync(gradeId, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }
}