using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using school_electronic_magazine.DTO;
using school_electronic_magazine.DTO.Requests;
using school_electronic_magazine.Services.Auth;
using school_electronic_magazine.Services.Token;
namespace school_electronic_magazine.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public AuthController(IUserService userService, ITokenService tokenService) // <- интерфейс
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("/login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserAuthLoginRequest userAuthLoginRequest)
    {
        var result = await _userService.GetUserByLoginAndPasswordAsync(userAuthLoginRequest);

        return Ok(new { result });
    }

    [HttpPost]
    [Route("/refreshToken")]
    public IActionResult CheckToken([FromBody] RefreshTokenRequest request)
    {
        var accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        
        if (!_tokenService.CheckToken(accessToken))
            return Unauthorized(new { error = "Access token недействителен или истек" });

        var userId = _tokenService.GetUserIdFromToken(accessToken);
        
        if (!_tokenService.ValidateRefreshToken(userId, request.RefreshToken))
            return Unauthorized(new { error = "Refresh token недействителен" });
        
        var roles = _tokenService.GetRolesFromToken(accessToken);

        var newAccessToken = _tokenService.GenerateAccessToken(userId, roles);
        
        return Ok(new { accessToken = newAccessToken });
    }

    [HttpPost]
    [Route("/register")]
    public async Task<IActionResult> Register([FromBody] UserDTO userDto)
    {
        var user = await _userService.CreateUserAsync(userDto);
        return Ok(user);
    }
}