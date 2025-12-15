using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repositories;
namespace school_electronic_magazine.Services;

public class GroupService(IGenericRepository<Models.Group> groupRepository, IGenericRepository<Student> studentRepository, IGenericRepository<SchoolClass> schoolClassRepository) : IGroupService
{

    public async Task AddGroupAsync(GroupRequestPayload payload)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        if (payload.ClassId.HasValue)
        {
            var currentYear = DateTime.UtcNow.Year;

            var existingGroup = await groupRepository.Query()
                .FirstOrDefaultAsync(g => g.ClassId == payload.ClassId 
                                          && g.CreationDate.Year == currentYear);

            if (existingGroup != null)
                throw new InvalidOperationException("Для текущего года группа с этим классом уже существует");
        }

        var group = new Models.Group
        {
            ClassId = payload.ClassId ?? 0,
            CreationDate = DateTime.UtcNow
        };

        await groupRepository.AddAsync(group);
        await groupRepository.SaveChangesAsync();
    }

    public async Task UpdateGroupAsync(long groupId, GroupRequestPayload payload)
    {
        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null)
            throw new InvalidOperationException("Группа не найдена");

        group.ClassId = payload.ClassId ?? 0;

        await groupRepository.UpdateAsync(group);
        await groupRepository.SaveChangesAsync();
    }

    // Если нужно, я могу убрать удаление, но по ТЗ у нас должен быть CRUD на все сущности
    public async Task DeleteGroupAsync(long groupId)
    {
        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null)
            throw new InvalidOperationException("Группа не найдена");

        var hasClasses = await schoolClassRepository.Query()
            .AnyAsync(sc => sc.ClassId == groupId);
        if (hasClasses)
            throw new InvalidOperationException("На группу ссылаются классы, удаление невозможно");

        var hasStudents = await studentRepository.Query()
            .AnyAsync(s => s.GroupId == groupId);
        if (hasStudents)
            throw new InvalidOperationException("В группе есть студенты, удаление невозможно");

        await groupRepository.DeleteAsync(groupId);
        await groupRepository.SaveChangesAsync();
    }
}