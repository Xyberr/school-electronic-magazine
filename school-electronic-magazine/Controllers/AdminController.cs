using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using school_electronic_magazine.Services.Auth;

namespace school_electronic_magazine.Controllers;


/*
 * Каждый эндпоинт позвращает 204 по прозьбе фронта
 */

[ApiController]
[Route("api/[controller]")]
public class AdminController(IUserService userService) : ControllerBase
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
}