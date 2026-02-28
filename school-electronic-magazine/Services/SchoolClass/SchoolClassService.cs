using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;

namespace school_electronic_magazine.Services;

public class SchoolClassService(IGenericRepository<SchoolClass> schoolClassRepository) : ISchoolClassService
{
    public async Task CreateSchoolClassAsync(SchoolClassRequestPayload schoolClassRequestPayload, CancellationToken cancellationToken)
    {
        if(schoolClassRequestPayload == null)
            throw new ArgumentNullException(nameof(schoolClassRequestPayload));
        
        var schoolClass = new SchoolClass
        {
            GroupLetter = schoolClassRequestPayload.GroupLabel,
            ClassNumber = schoolClassRequestPayload.ClassNumber,
            CreationDate = DateTime.UtcNow,
            EnterDateForStudents = (uint)DateTime.UtcNow.Year,
            GroupId = schoolClassRequestPayload.GroupId,
            ModificationDate = DateTime.UtcNow
        };
        
        await schoolClassRepository.AddAsync(schoolClass, cancellationToken);
        await schoolClassRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveSchoolClassAsync(long schoolClassId, CancellationToken cancellationToken)
    {
        var schoolClass = await schoolClassRepository.GetByIdAsync(schoolClassId, cancellationToken);
        if (schoolClass == null)
            throw new InvalidOperationException("Класс не найден");

        if (schoolClass.GroupId != null)
            throw new InvalidOperationException("Невозможно удалить класс, так как он привязан к группе");

        if (schoolClass.Lessons.Any())
            throw new InvalidOperationException("Невозможно удалить класс, так как к нему привязаны уроки");

        await schoolClassRepository.DeleteByIdAsync(schoolClassId, cancellationToken);
        await schoolClassRepository.SaveChangesAsync(cancellationToken);

    }

    public async Task UpdateSchoolClassAsync(
        long schoolClassId,
        SchoolClassRequestPayload schoolClassRequestPayload,
        CancellationToken cancellationToken)
    {
        if (schoolClassRequestPayload == null)
            throw new ArgumentNullException(nameof(schoolClassRequestPayload));

        var schoolClass = await schoolClassRepository
            .GetByIdAsync(schoolClassId, cancellationToken, asNoTracking: false);

        if (schoolClass == null)
            throw new InvalidOperationException(
                $"School class with id {schoolClassId} not found");

        schoolClass.GroupLetter = schoolClassRequestPayload.GroupLabel;
        schoolClass.ClassNumber = schoolClassRequestPayload.ClassNumber;
        schoolClass.EnterDateForStudents =
            (uint)schoolClassRequestPayload.EnterDate.Year;

        await schoolClassRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<SchoolClass> GetSchoolClassByIdAsync(long schoolClassId, CancellationToken cancellationToken)
    {
        return await schoolClassRepository
            .GetByIdAsync(schoolClassId, cancellationToken);
    }
}