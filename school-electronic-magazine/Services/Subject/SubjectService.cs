using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Repositories.Subject;

namespace school_electronic_magazine.Services.Subject;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;

    public SubjectService(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task AddSubjectAsync(SubjectRequestPayload payload)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        var exists = await _subjectRepository.GetByNameAsync(payload.Name);
        if (exists != null)
            throw new InvalidOperationException("Предмет с таким названием уже существует");

        var subject = new Models.Subject
        {
            Name = payload.Name,
            Lesson = null,
            CreationDate = DateTime.UtcNow,

        };

        await _subjectRepository.AddAsync(subject);
        await _subjectRepository.SaveChangesAsync();
    }

    public async Task UpdateSubjectAsync(long subjectId, SubjectRequestPayload payload)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        var subject = await _subjectRepository.GetByIdAsync(subjectId);
        if (subject == null)
            throw new InvalidOperationException("Предмет не найден");

        var exists = await _subjectRepository.GetByNameAsync(payload.Name);
        if (exists != null && exists.Id != subjectId)
            throw new InvalidOperationException("Предмет с таким названием уже существует");

        subject.Name = payload.Name;

        await _subjectRepository.UpdateAsync(subject);
        await _subjectRepository.SaveChangesAsync();
    }

    public async Task DeleteSubjectAsync(long subjectId)
    {
        var subject = await _subjectRepository.GetByIdAsync(subjectId);
        if (subject == null)
            throw new InvalidOperationException("Предмет не найден");

        await _subjectRepository.DeleteAsync(subjectId);
        await _subjectRepository.SaveChangesAsync();
    }
}