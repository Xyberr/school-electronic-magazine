using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using school_electronic_magazine.DTO.Requests; 
using school_electronic_magazine.Services;


namespace school_electronic_magazine.Controllers;


/*
 * Каждый эндпоинт позвращает 204 по просьбе фронта
 *
 * Так же фронт ещё раз сказал, что пока что, ему ничего не нужно, но не обещал в будущем, не потребует
 */

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController(IUserService userService, ISchoolClassService schoolClassService, ISubjectService subjectService, IGroupService groupService, ILessonService lessonService) : ControllerBase
{
    [HttpPost("assignRolesAsyncRoles/{userId:long}")]
    public async Task<IActionResult> AssignRolesAsyncRoles([FromBody] List<long> roleIds, long userId, CancellationToken cancellationToken)
    {
        await userService.AssignRolesAsync(userId, roleIds, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete("removeRoles/{userId:long}")]
    public async Task<IActionResult> RemoveRoles(long userId, List<long>? roleIds, CancellationToken cancellationToken)
    {
        await userService.RemoveRolesAsync(userId, roleIds, cancellationToken);
        return NoContent();
    }

    [HttpDelete("removeUserById/{userId:long}")]
    public async Task<IActionResult> RemoveUserByIdAsync(long userId, CancellationToken cancellationToken)
    {
        await userService.RemoveUserByIdAsync(userId, cancellationToken);
        return NoContent();
    }

    [HttpPost("addSchoolClass")]
    public async Task<IActionResult> addSchoolClass(SchoolClassRequestPayload schoolClassRequestPayload, CancellationToken cancellationToken)
    {
        await schoolClassService.CreateSchoolClassAsync(schoolClassRequestPayload, cancellationToken);
        return NoContent();
    }

    [HttpDelete("removeSchoolClass/{SchoolClassId:long}")]
    public async Task<IActionResult> RemoveSchoolClass(long SchoolClassId, CancellationToken cancellationToken)
    {
        await schoolClassService.RemoveSchoolClassAsync(SchoolClassId, cancellationToken);
        return NoContent();
    }

    [HttpPatch("updateSchoolClass/{SchoolClassId:long}")]
    [Authorize(Roles = "Admin, Teacher")]
    public async Task<IActionResult> UpdateSchoolClassAsync(long SchoolClassId, [FromBody]SchoolClassRequestPayload schoolClassRequestPayload, CancellationToken cancellationToken)
    {
        await schoolClassService.UpdateSchoolClassAsync(SchoolClassId ,schoolClassRequestPayload,  cancellationToken);
        return NoContent();
    }

    [HttpPost("addSubject")]
    public async Task<IActionResult> AddSubject([FromBody] SubjectRequestPayload subjectRequestPayload, CancellationToken cancellationToken)
    {
        await subjectService.AddSubjectAsync(subjectRequestPayload, cancellationToken);
        return NoContent();
    }

    [HttpPatch("updateSubject{SubjectId:long}")]
    public async Task<IActionResult> UpdateSubject(long SubjectId, [FromBody] SubjectRequestPayload subjectRequestPayload,  CancellationToken cancellationToken)
    {
        await subjectService.UpdateSubjectAsync(SubjectId, subjectRequestPayload, cancellationToken);
        return NoContent();
    }

    [HttpDelete("removeSubject/{subjectId:long}")]
    public async Task<IActionResult> RemoveSubject(long SubjectId, CancellationToken cancellationToken)
    {
        await subjectService.DeleteSubjectAsync(SubjectId, cancellationToken);
        return NoContent();
    }
    
    [HttpPost("addGroup")]
    public async Task<IActionResult> AddGroup([FromBody] GroupRequestPayload payload, CancellationToken cancellationToken)
    {
        await groupService.AddGroupAsync(payload, cancellationToken);
        return NoContent();
    }

    [HttpPatch("updateGroup/{groupId:long}")]
    public async Task<IActionResult> UpdateGroup(long groupId, [FromBody] GroupRequestPayload payload, CancellationToken cancellationToken)
    {
        await groupService.UpdateGroupAsync(groupId, payload, cancellationToken);
        return NoContent();
    }

    [HttpDelete("deleteGroup/{groupId:long}")]
    public async Task<IActionResult> DeleteGroup(long groupId, CancellationToken cancellationToken)
    {
        await groupService.DeleteGroupAsync(groupId, cancellationToken);
        return NoContent();
    }

    [HttpPost("AddLesson")]
    public async Task<IActionResult> AddLesson(LessonRequestPayload payload,  CancellationToken cancellationToken)
    {
        await lessonService.AddLessonAsync(payload, cancellationToken);
        return NoContent();
    }

    [HttpDelete("RemoveLesson/{lessonId:long}")] 
    public async Task<IActionResult> RemoveLesson(long lessonId, CancellationToken cancellationToken)
    {
        await lessonService.DeleteLessonAsync(lessonId, cancellationToken);
        return NoContent();
    }

    [HttpPost("updateLesson/{lessonId:long}")]
    public async Task<IActionResult> UpdateLesson(long lessonId,LessonRequestPayload payload,  CancellationToken cancellationToken)
    {
        await lessonService.UpdateLessonAsync(lessonId, payload, cancellationToken);
        return NoContent();
    }
    
    
}