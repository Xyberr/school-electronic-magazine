using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using school_electronic_magazine.DTO;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Services;
namespace school_electronic_magazine.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserService userService, ITokenService tokenService) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] UserAuthRequestPayload userAuthRequestPayload)
        => Ok(await userService.AuthorizeUserAsync(userAuthRequestPayload));

    [AllowAnonymous]
    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequestPayload payload)
    {
        var oldToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        var tokensRequestPayload = await tokenService.RotateRefreshTokenAsync(oldToken, payload.RefreshToken);

        return Ok(tokensRequestPayload);
    }

    [HttpGet("test-hello-teacher")]
    [Authorize(Roles = "Teacher")]
    public IActionResult TestHelloTeacher()
        => Ok("hello teacher!");

    [HttpGet("test-roles")]
    [Authorize(Roles = "Teacher, Student, Admin")]
    public IActionResult TestRolesTeacher()
        => Ok("hello!");
    
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequestPayload userRegisterRequestPayload)
        => Ok(await userService.CreateUserAsync(userRegisterRequestPayload));
}