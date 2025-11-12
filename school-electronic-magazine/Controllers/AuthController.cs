using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using school_electronic_magazine.DTO;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Services.Auth;
using school_electronic_magazine.Services.Token;
namespace school_electronic_magazine.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserService userService, ITokenService tokenService) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly ITokenService _tokenService = tokenService;

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserAuthRequestPayload  userAuthRequestPayload)
       => Ok(await _userService.GetUserByLoginAsync(userAuthRequestPayload));

    [HttpPost]
    [Authorize(Roles = "Student, Teacher, Admin")]
    [Route("refreshToken")]
    public IActionResult CheckToken([FromBody] RefreshTokenRequestPayload refreshTokenRequestPayload)
    {
        var accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        
        if (!_tokenService.VerifyToken(accessToken))
            return Unauthorized(new { error = "Access token недействителен или истек" });

        var userId = _tokenService.GetUserIdFromToken(accessToken);
        
        if (!_tokenService.ValidateRefreshToken(userId, refreshTokenRequestPayload.RefreshToken))
            return Unauthorized(new { error = "Refresh token недействителен" });
        
        var roles = _tokenService.GetRolesFromToken(accessToken);

        var newAccessToken = _tokenService.GenerateAccessToken(userId, roles);
        
        return Ok(new { accessToken = newAccessToken });
    }

    [HttpGet]
    [Authorize(Roles = "Teacher")]
    [Route("test-hello-teacher")]
    public IActionResult TestHelloTeacher()
    {
        return Ok("hello teacher!");
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequestPayload userRegisterRequestPayload)
    {
        var user = await _userService.CreateUserAsync(userRegisterRequestPayload);
        return Ok(user);
    }
}