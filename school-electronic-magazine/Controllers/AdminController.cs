using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Repositories.SchoolClass;
using school_electronic_magazine.Services.Auth;
using school_electronic_magazine.Services.Group;
using school_electronic_magazine.Services.SchoolClass;
using school_electronic_magazine.Services.Subject;

namespace school_electronic_magazine.Controllers;


/*
 * Каждый эндпоинт позвращает 204 по прозьбе фронта
 */

[ApiController]
[Route("api/[controller]")]
public class AdminController(IUserService userService, ISchoolClassService schoolClassService, ISubjectService subjectService, IGroupService groupService) : ControllerBase
{
    [HttpPost("addRoles/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddRoles([FromBody] List<string> roles, long userId)
    {
        await userService.AddRolesAsync(userId, roles);
        return NoContent();
    }

    [HttpPost("removeRoles/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveRoles(long userId, List<string> roles)
    {
        await userService.RemoveRolesAsync(userId, roles);
        return NoContent();
    }

    [HttpPost("removeUserById/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveUserByIdAsync(long userId)
    {
        await userService.RemoveUserByIdAsync(userId);
        return NoContent();
    }

    [HttpPost("addSchoolClass")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> addSchoolClass(SchoolClassRequestPayload schoolClassRequestPayload)
    {
        await schoolClassService.CreateSchoolClassAsync(schoolClassRequestPayload);
        return NoContent();
    }

    [HttpPost("removeSchoolClass")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveSchoolClass(long SchoolClassId)
    {
        await schoolClassService.RemoveSchoolClassAsync(SchoolClassId);
        return NoContent();
    }

    [HttpPost("updateSchoolClass")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateSchoolClass(long SchoolClassId, [FromBody]SchoolClassRequestPayload schoolClassRequestPayload)
    {
        await schoolClassService.UpdateSchoolClass(SchoolClassId ,schoolClassRequestPayload);
        return NoContent();
    }

    [HttpPost("addSubject")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddSubject([FromBody] SubjectRequestPayload subjectRequestPayload)
    {
        await subjectService.AddSubjectAsync(subjectRequestPayload);
        return NoContent();
    }

    [HttpPost("updateSubject{SubjectId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateSubject(long SubjectId, [FromBody] SubjectRequestPayload subjectRequestPayload)
    {
        await subjectService.UpdateSubjectAsync(SubjectId, subjectRequestPayload);
        return NoContent();
    }

    [HttpPost("removeSubject/{subjectId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveSubject(long SubjectId)
    {
        subjectService.DeleteSubjectAsync(SubjectId);
        return NoContent();
    }
    
    [HttpPost("addGroup")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddGroup([FromBody] GroupRequestPayload payload)
    {
        await groupService.AddGroupAsync(payload);
        return NoContent();
    }

    [HttpPut("updateGroup/{groupId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateGroup(long groupId, [FromBody] GroupRequestPayload payload)
    {
        await groupService.UpdateGroupAsync(groupId, payload);
        return NoContent();
    }

    [HttpDelete("deleteGroup/{groupId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteGroup(long groupId)
    {
        await groupService.DeleteGroupAsync(groupId);
        return NoContent();
    }
}