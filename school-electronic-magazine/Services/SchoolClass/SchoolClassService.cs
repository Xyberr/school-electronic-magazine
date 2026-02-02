using Microsoft.AspNetCore.Mvc;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;

namespace school_electronic_magazine.Services;

public class SchoolClassService(IGenericRepository<Models.SchoolClass> genericSchoolClassRepository) : ISchoolClassService
{
    public async Task CreateSchoolClassAsync(SchoolClassRequestPayload schoolClassRequestPayload, CancellationToken cancellationToken)
    {
        if(schoolClassRequestPayload == null)
            throw new ArgumentNullException(nameof(schoolClassRequestPayload));
        
        var schoolClass = new Models.SchoolClass
        {
            GroupLetter = schoolClassRequestPayload.GroupLabel,
            ClassNumber = schoolClassRequestPayload.ClassNumber,
            CreationDate = DateTime.UtcNow,
            EnterDateForStudents = (uint)DateTime.UtcNow.Year,
            GroupId = schoolClassRequestPayload.GroupId,
            ModificationDate = DateTime.UtcNow
        };
        
        await genericSchoolClassRepository.AddAsync(schoolClass, cancellationToken);
        await genericSchoolClassRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveSchoolClassAsync(long schoolClassId, CancellationToken cancellationToken)
    {
        if(schoolClassId < 0)
            throw new ArgumentOutOfRangeException(nameof(schoolClassId));
        
        await genericSchoolClassRepository.DeleteAsync(schoolClassId, cancellationToken);
        await genericSchoolClassRepository.SaveChangesAsync(cancellationToken);

    }

    public async Task UpdateSchoolClass(long schoolClassId, SchoolClassRequestPayload schoolClassRequestPayload, CancellationToken cancellationToken)
    {
        if(schoolClassRequestPayload == null)
            throw new ArgumentNullException(nameof(schoolClassRequestPayload));

        var schoolClass = await genericSchoolClassRepository.GetByIdAsync(schoolClassId,cancellationToken);
        if(schoolClass == null)
            throw new InvalidOperationException($"School class with id {schoolClassId} not found");
        
        schoolClass.GroupLetter = schoolClassRequestPayload.GroupLabel;
        schoolClass.ClassNumber = schoolClassRequestPayload.ClassNumber;
        schoolClass.EnterDateForStudents = (uint)schoolClassRequestPayload.EnterDate.Year;
        
        
        genericSchoolClassRepository.Update(schoolClass);
        await genericSchoolClassRepository.SaveChangesAsync(cancellationToken);

    }

    public async Task<SchoolClass> GetSchoolClassByIdAsync(long schoolClassId, CancellationToken cancellationToken)
    {
        return await genericSchoolClassRepository
            .GetByIdAsync(schoolClassId, cancellationToken);
    }
}