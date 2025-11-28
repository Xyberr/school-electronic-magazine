using school_electronic_magazine.DTO.Requests;

namespace school_electronic_magazine.Services.Group;

public interface IGroupService
{
    Task AddGroupAsync(GroupRequestPayload payload);
    Task UpdateGroupAsync(long groupId, GroupRequestPayload payload);
    Task DeleteGroupAsync(long groupId);
}