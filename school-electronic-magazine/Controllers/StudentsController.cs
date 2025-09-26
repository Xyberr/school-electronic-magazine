using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using school_electronic_magazine.DTO;
using school_electronic_magazine.Models;
using school_electronic_magazine.Services.Student;

namespace school_electronic_magazine.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> GetAllStudents()
    {
        return Ok(await _studentService.GetAllStudentsAsync());
    }
    
    [Authorize(Roles = "Admin,Teacher,Student")]
    [HttpGet("get_student_/{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        try
        {
            var student = await _studentService.GetStudentById(id);
            return Ok(student);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("add_student")]
    public async Task<IActionResult> AddStudent(StudentDto studentDto)
    {
        return Ok(await _studentService.Create(studentDto));
    }

    [Authorize(Roles = "Admin,Teacher")]
    [HttpPut("update_student")]
    public async Task<IActionResult> UpdateStudent(int id, [FromBody]StudentDto studentDto)
    {
        return Ok(await _studentService.Update(id, studentDto));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete_student_/{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        return Ok(await _studentService.Delete(id));
    }
    
}
