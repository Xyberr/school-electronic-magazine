using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;

namespace school_electronic_magazine.Services;

public class SubjectService(ISubjectRepository subjectRepository) :  ISubjectService
{

    public async Task AddSubjectAsync(SubjectRequestPayload payload)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        var exists = await subjectRepository.GetByNameAsync(payload.Name);
        if (exists != null)
            throw new InvalidOperationException("Предмет с таким названием уже существует");

        var subject = new Subject
        {
            Name = payload.Name,
            CreationDate = DateTime.UtcNow,

        };

        await subjectRepository.AddAsync(subject);
        await subjectRepository.SaveChangesAsync();
    }

    public async Task UpdateSubjectAsync(long subjectId, SubjectRequestPayload payload)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        var subject = await subjectRepository.GetByIdAsync(subjectId);
        if (subject == null)
            throw new InvalidOperationException("Предмет не найден");

        var exists = await subjectRepository.GetByNameAsync(payload.Name);
        if (exists != null && exists.Id != subjectId)
            throw new InvalidOperationException("Предмет с таким названием уже существует");

        subject.Name = payload.Name;

        await subjectRepository.UpdateAsync(subject);
        await subjectRepository.SaveChangesAsync();
    }

    public async Task DeleteSubjectAsync(long subjectId)
    {
        var subject = await subjectRepository.GetByIdAsync(subjectId);
        if (subject == null)
            throw new InvalidOperationException("Предмет не найден");

        await subjectRepository.DeleteAsync(subjectId);
        await subjectRepository.SaveChangesAsync();
    }
}