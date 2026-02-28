using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;

namespace school_electronic_magazine.Services;

public class SubjectService(ISubjectRepository subjectRepository) :  ISubjectService
{

    public async Task AddSubjectAsync(SubjectRequestPayload payload, CancellationToken cancellationToken)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        var exists = await subjectRepository.GetByNameAsync(payload.Name, cancellationToken);
        if (exists != null)
            throw new InvalidOperationException("Предмет с таким названием уже существует");

        var subject = new Subject
        {
            Name = payload.Name,
            CreationDate = DateTime.UtcNow,
            ModificationDate = DateTime.UtcNow,
        };
        
        await subjectRepository.AddAsync(subject, cancellationToken);
        await subjectRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateSubjectAsync(long subjectId, SubjectRequestPayload payload, CancellationToken cancellationToken)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        var subject = await subjectRepository.GetByIdAsync(subjectId, cancellationToken);
        if (subject == null)
            throw new InvalidOperationException("Предмет не найден");

        var exists = await subjectRepository.Query()
            .AnyAsync(subject => subject.Name == payload.Name && subject.Id != subjectId,  cancellationToken);
        
        if (exists)
            throw new InvalidOperationException("Предмет с таким названием уже существует");

        subject.Name = payload.Name;
        subjectRepository.Update(subject);
        await subjectRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteSubjectAsync(long subjectId, CancellationToken cancellationToken)
    {
        var subject = await subjectRepository.GetByIdAsync(subjectId, cancellationToken);

        if (subject == null)
            throw new InvalidOperationException("Предмет не найден");

        subjectRepository.DeleteByIdAsync(subjectId, cancellationToken);
        await subjectRepository.SaveChangesAsync(cancellationToken);
    }
}