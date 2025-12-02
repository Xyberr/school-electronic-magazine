using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Repositories;

namespace school_electronic_magazine.Services.Grade;

public class GradeService(IGenericRepository<Models.Grade> repository) : IGradeService
{
    public async Task AddGradeAsync(GradeRequestPayload payload)
    {
        if(payload == null)
            throw new ArgumentNullException("Данные пустые");

        var grade = new Models.Grade
        {
            CreationDate = DateTime.Now,
            StudentId = payload.StudentId,
            SchoolClassId = payload.SchoolClassId,
            LessonId = payload.LessonId,
            Value = payload.Value,
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
    }

    public async Task DeleteGradeAsync(long gradeId)
    {
        if (gradeId == null)
            throw new ArgumentNullException("id не может быть null");
        
        await repository.DeleteAsync(gradeId);
    }
}