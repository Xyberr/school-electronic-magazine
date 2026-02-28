using school_electronic_magazine.DTO.Requests;

namespace school_electronic_magazine.Services;

public interface IGroupService
{
    Task AddGroupAsync(GroupRequestPayload payload, CancellationToken cancellationToken);
    Task UpdateGroupAsync(long groupId, GroupRequestPayload payload, CancellationToken cancellationToken);
    Task DeleteGroupAsync(long groupId, CancellationToken cancellationToken);
}