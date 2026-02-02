using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;

namespace school_electronic_magazine.Services;

public class GroupService(IGenericRepository<Models.Group> groupRepository, IGenericRepository<Student> studentRepository, IGenericRepository<SchoolClass> schoolClassRepository) : IGroupService
{

    public async Task AddGroupAsync(GroupRequestPayload payload, CancellationToken cancellationToken)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        var currentYear = DateTime.UtcNow.Year;

        // Диапазон поиска
        var yearStart = new DateTime(currentYear, 1, 1);
        var yearEnd = yearStart.AddYears(1);

        var exists = await groupRepository.Query()
            .AnyAsync(g =>
                g.ClassId == payload.ClassId &&
                g.CreationDate >= yearStart &&
                g.CreationDate < yearEnd);

        if (exists)
            throw new InvalidOperationException("Группа для данного класса в этом году уже существует");

        var group = new Models.Group
        {
            ClassId = payload.ClassId,
            CreationDate = DateTime.UtcNow,
            ModificationDate = DateTime.UtcNow
        };

        await groupRepository.AddAsync(group, cancellationToken);
        await groupRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateGroupAsync(long groupId, GroupRequestPayload payload, CancellationToken cancellationToken)
    {
        var group = await groupRepository.GetByIdAsync(groupId,cancellationToken);
        if (group == null)
            throw new InvalidOperationException("Группа не найдена");

        group.ClassId = payload.ClassId;

        groupRepository.Update(group);
        await groupRepository.SaveChangesAsync(cancellationToken);
    }

    // По ТЗ у нас должен быть CRUD на все сущности
    public async Task DeleteGroupAsync(long groupId, CancellationToken cancellationToken)
    {
        var groupData = await groupRepository.Query()
            .Where(group => group.Id == groupId)
            .Select(group => new
            {
                group.Id,
                HasClasses = group.SchoolClasses.Any(),
                HasStudents = group.Students.Any()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (groupData == null)
            throw new InvalidOperationException("Группа не найдена");

        if (groupData.HasClasses)
            throw new InvalidOperationException("На группу ссылаются классы, удаление невозможно");

        if (groupData.HasStudents)
            throw new InvalidOperationException("В группе есть студенты, удаление невозможно");

        await groupRepository.DeleteAsync(groupId, cancellationToken);
        await groupRepository.SaveChangesAsync(cancellationToken);
    }
}