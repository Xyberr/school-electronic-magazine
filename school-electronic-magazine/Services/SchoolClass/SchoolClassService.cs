using Microsoft.AspNetCore.Mvc;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Repositories;

namespace school_electronic_magazine.Services.SchoolClass;

public class SchoolClassService(IGenericRepository<Models.SchoolClass> genericSchoolClassRepository) : ISchoolClassService
{
    public async Task CreateSchoolClassAsync(SchoolClassRequestPayload schoolClassRequestPayload)
    {
        if(schoolClassRequestPayload == null)
            throw new ArgumentNullException(nameof(schoolClassRequestPayload));
        
        var SchoolClass = new Models.SchoolClass
        {
            GroupLabel = schoolClassRequestPayload.GroupLabel,
            ClassNumber = schoolClassRequestPayload.ClassNumber,
            GroupId = schoolClassRequestPayload.GroupId,
            EnterDate = schoolClassRequestPayload.EnterDate,
            CreationDate = DateTime.UtcNow
        };

        await genericSchoolClassRepository.AddAsync(SchoolClass);
        await genericSchoolClassRepository.SaveChangesAsync();
    }

    public async Task RemoveSchoolClassAsync(long schoolClassId)
    {
        if(schoolClassId < 0)
            throw new ArgumentOutOfRangeException(nameof(schoolClassId));
        
        await genericSchoolClassRepository.DeleteAsync(schoolClassId);
        await genericSchoolClassRepository.SaveChangesAsync();

    }

    public async Task UpdateSchoolClass(long SchoolClassId, SchoolClassRequestPayload schoolClassRequestPayload)
    {
        if(schoolClassRequestPayload == null)
            throw new ArgumentNullException(nameof(schoolClassRequestPayload));

        var SchoolClass = await genericSchoolClassRepository.GetByIdAsync(SchoolClassId);
        if(SchoolClass == null)
            throw new InvalidOperationException($"School class with id {SchoolClassId} not found");
        
        SchoolClass.GroupLabel = schoolClassRequestPayload.GroupLabel;
        SchoolClass.ClassNumber = schoolClassRequestPayload.ClassNumber;
        SchoolClass.EnterDate = schoolClassRequestPayload.EnterDate;
        SchoolClass.GroupId = schoolClassRequestPayload.GroupId;
        
        
        await genericSchoolClassRepository.UpdateAsync(SchoolClass);
        await genericSchoolClassRepository.SaveChangesAsync();

    }

    public async Task<Models.SchoolClass> GetSchoolClassByIdAsync(long schoolClassId)
    {
         await genericSchoolClassRepository.GetByIdAsync(schoolClassId);
        await genericSchoolClassRepository.SaveChangesAsync();
        return await genericSchoolClassRepository.GetByIdAsync(schoolClassId);
    }
}