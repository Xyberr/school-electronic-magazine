using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;

namespace school_electronic_magazine.Services;

public class GradeService(IGenericRepository<Grade> repository, IGenericRepository<Student> studentRepository, IGenericRepository<Lesson> lessonRepository) : IGradeService
{
    public async Task AddGradeAsync(GradeRequestPayload payload)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        var student = await studentRepository.GetByIdAsync(payload.StudentId);
        if (student == null)
            throw new InvalidOperationException("Студент не найден");

        var lesson = await lessonRepository.GetByIdAsync(payload.LessonId);
        if (lesson == null)
            throw new InvalidOperationException("Урок не найден");

        var existingGrade = await repository.Query()
            .FirstOrDefaultAsync(g => g.StudentId == student.Id && g.LessonId == lesson.Id);

        if (existingGrade != null)
            throw new InvalidOperationException("Оценка за этот урок уже проставлена");

        var grade = new Models.Grade
        {
            CreationDate = DateTime.UtcNow,
            StudentId = student.Id,
            SchoolClassId = student.GroupId,
            LessonId = lesson.Id,
            Value = payload.Value
        };

        await repository.AddAsync(grade);
        await repository.SaveChangesAsync();
    }

    public async Task UpdateGradeAsync(long gradeId, GradeRequestPayload payload)
    {
        if (payload == null)
            throw new ArgumentNullException("Данные пустые");
        
        var grade = await repository.GetByIdAsync(gradeId);
        
        grade.Value = payload.Value;
        grade.StudentId = payload.StudentId;
        grade.SchoolClassId = payload.SchoolClassId;
        grade.LessonId = payload.LessonId;

        await repository.UpdateAsync(grade);
        await repository.SaveChangesAsync();
    }

    public async Task DeleteGradeAsync(long gradeId)
    {
        if (gradeId == null)
            throw new ArgumentNullException("id не может быть null");
        
        await repository.DeleteAsync(gradeId);
        await repository.SaveChangesAsync();
    }
}