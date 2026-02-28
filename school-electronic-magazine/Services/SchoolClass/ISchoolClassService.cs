using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Models;

namespace school_electronic_magazine.Services;

public interface ISchoolClassService
{
    Task CreateSchoolClassAsync(SchoolClassRequestPayload schoolClassRequestPayload, CancellationToken cancellationToken);
    Task RemoveSchoolClassAsync(long schoolClassId, CancellationToken cancellationToken);
    Task UpdateSchoolClassAsync(long SchoolClassId,SchoolClassRequestPayload schoolClassRequestPayload, CancellationToken cancellationToken);
    Task<SchoolClass> GetSchoolClassByIdAsync(long schoolClassId, CancellationToken cancellationToken);
}