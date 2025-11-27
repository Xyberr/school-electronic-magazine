using Microsoft.AspNetCore.Mvc;
using school_electronic_magazine.DTO;
using school_electronic_magazine.DTO.Requests;

namespace school_electronic_magazine.Services.SchoolClass;

public interface ISchoolClassService
{
    Task CreateSchoolClassAsync(SchoolClassRequestPayload schoolClassRequestPayload);
    Task RemoveSchoolClassAsync(long schoolClassId);
    Task UpdateSchoolClass(long SchoolClassId,SchoolClassRequestPayload schoolClassRequestPayload);
    Task<Models.SchoolClass> GetSchoolClassAsync(long schoolClassId);
}