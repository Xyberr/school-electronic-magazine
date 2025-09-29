using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using school_electronic_magazine.DTO;
using school_electronic_magazine.Models;
using school_electronic_magazine.Services.Student;

namespace school_electronic_magazine.Controllers;

[Controller]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _userService.GetAllAsync());
    }

    [HttpGet]
    [Route("getByRole/{role}")]
    public async Task<IActionResult> GetByRole(string role)
    {
        return Ok(await _userService.GetUserByRole(role));
    }

    [HttpGet]
    [Route("getByClass/{classId}")]
    public async Task<IActionResult> GetByClass(long classId)
    {
        return Ok(await _userService.GetUserByClass(classId));
    }

    [HttpGet]
    [Route("getUserById/{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        return Ok(await _userService.GetByIdAsync(id));
    }

    [HttpGet]
    [Route("getUserByName/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        return Ok(await _userService.GetUserByName(name));
    }

    [HttpPost("createUser")]
    public async Task<IActionResult> CreateAsync([FromBody] UserDto userDto)
    {
        var createdUser = await _userService.CreateAsync(userDto);
        return Ok(createdUser);
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateAsync(long id, [FromBody] UserDto userDto)
    {
        return Ok(await _userService.UpdateAsync(id, userDto));
    }
    
    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        return Ok(await _userService.DeleteAsync(id));
    }
}