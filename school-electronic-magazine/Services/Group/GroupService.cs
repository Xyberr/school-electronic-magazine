using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Repositories;
namespace school_electronic_magazine.Services.Group;

public class GroupService(IGenericRepository<Models.Group> groupRepository) : IGroupService
{

    public async Task AddGroupAsync(GroupRequestPayload payload)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));
        

        var group = new Models.Group
        {
            ClassId = payload.ClassId,
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

        group.ClassId = payload.ClassId;

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