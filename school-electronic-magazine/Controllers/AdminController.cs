using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Repositories;
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
    public async Task<IActionResult> AssignRolesAsyncRoles([FromBody] List<string> roles, long userId)
    {
        await userService.AssignRolesAsync(userId, roles);
        return NoContent();
    }

    [HttpDelete("removeRoles/{userId:long}")]
    public async Task<IActionResult> RemoveRoles(long userId, List<string> roles)
    {
        await userService.RemoveRolesAsync(userId, roles);
        return NoContent();
    }

    [HttpDelete("removeUserById/{userId:long}")]
    public async Task<IActionResult> RemoveUserByIdAsync(long userId)
    {
        await userService.RemoveUserByIdAsync(userId);
        return NoContent();
    }

    [HttpPost("addSchoolClass")]
    public async Task<IActionResult> addSchoolClass(SchoolClassRequestPayload schoolClassRequestPayload)
    {
        await schoolClassService.CreateSchoolClassAsync(schoolClassRequestPayload);
        return NoContent();
    }

    [HttpDelete("removeSchoolClass/{SchoolClassId:long}")]
    public async Task<IActionResult> RemoveSchoolClass(long SchoolClassId)
    {
        await schoolClassService.RemoveSchoolClassAsync(SchoolClassId);
        return NoContent();
    }

    [HttpPatch("updateSchoolClass/{SchoolClassId:long}")]
    [Authorize(Roles = "Admin, Teacher")]
    public async Task<IActionResult> UpdateSchoolClass(long SchoolClassId, [FromBody]SchoolClassRequestPayload schoolClassRequestPayload)
    {
        await schoolClassService.UpdateSchoolClass(SchoolClassId ,schoolClassRequestPayload);
        return NoContent();
    }

    [HttpPost("addSubject")]
    public async Task<IActionResult> AddSubject([FromBody] SubjectRequestPayload subjectRequestPayload)
    {
        await subjectService.AddSubjectAsync(subjectRequestPayload);
        return NoContent();
    }

    [HttpPatch("updateSubject{SubjectId:long}")]
    public async Task<IActionResult> UpdateSubject(long SubjectId, [FromBody] SubjectRequestPayload subjectRequestPayload)
    {
        await subjectService.UpdateSubjectAsync(SubjectId, subjectRequestPayload);
        return NoContent();
    }

    [HttpDelete("removeSubject/{subjectId:long}")]
    public async Task<IActionResult> RemoveSubject(long SubjectId)
    {
        subjectService.DeleteSubjectAsync(SubjectId);
        return NoContent();
    }
    
    [HttpPost("addGroup")]
    public async Task<IActionResult> AddGroup([FromBody] GroupRequestPayload payload)
    {
        await groupService.AddGroupAsync(payload);
        return NoContent();
    }

    [HttpPatch("updateGroup/{groupId:long}")]
    public async Task<IActionResult> UpdateGroup(long groupId, [FromBody] GroupRequestPayload payload)
    {
        await groupService.UpdateGroupAsync(groupId, payload);
        return NoContent();
    }

    [HttpDelete("deleteGroup/{groupId:long}")]
    public async Task<IActionResult> DeleteGroup(long groupId)
    {
        await groupService.DeleteGroupAsync(groupId);
        return NoContent();
    }

    [HttpPost("AddLesson")]
    public async Task<IActionResult> AddLesson(LessonRequestPayload payload)
    {
        await lessonService.AddLessonAsync(payload);
        return NoContent();
    }

    [HttpDelete("RemoveLesson/{lessonId:long}")] 
    public async Task<IActionResult> RemoveLesson(long lessonId)
    {
        await lessonService.DeleteLessonAsync(lessonId);
        return NoContent();
    }

    [HttpPost("updateLesson/{lessonId:long}")]
    public async Task<IActionResult> UpdateLesson(long lessonId,LessonRequestPayload payload)
    {
        await lessonService.UpdateLessonAsync(lessonId, payload);
        return NoContent();
    }
    
    
}