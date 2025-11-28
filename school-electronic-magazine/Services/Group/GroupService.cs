using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Repositories.Group;

namespace school_electronic_magazine.Services.Group;

public class GroupService(IGroupRepository groupRepository) : IGroupService
{

    public async Task AddGroupAsync(GroupRequestPayload payload)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        if (await groupRepository.ExistsAsync(payload.ClassId, payload.StudentId))
            throw new InvalidOperationException("Такая группа уже существует");

        var group = new Models.Group
        {
            ClassId = payload.ClassId,
            StudentId = payload.StudentId,
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

        // Проверка на дубль
        if (await groupRepository.ExistsAsync(payload.ClassId, payload.StudentId)
            && (group.ClassId != payload.ClassId || group.StudentId != payload.StudentId))
        {
            throw new InvalidOperationException("Такая группа уже существует");
        }

        group.ClassId = payload.ClassId;
        group.StudentId = payload.StudentId;

        await groupRepository.UpdateAsync(group);
        await groupRepository.SaveChangesAsync();
    }

    public async Task DeleteGroupAsync(long groupId)
    {
        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null)
            throw new InvalidOperationException("Группа не найдена");

        await groupRepository.DeleteAsync(groupId);
        await groupRepository.SaveChangesAsync();
    }
}