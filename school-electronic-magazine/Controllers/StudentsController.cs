using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using school_electronic_magazine.DTO;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repository.Student;
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
    public IActionResult GetAllStudents()
    {
        return Ok(_studentService.GetAllStudents());
    }
    
    [HttpGet("get_student_/{id}")]
    public IActionResult GetStudentById(int id)
    {
        try
        {
            var student = _studentService.GetStudentById(id);
            return Ok(student);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("add_student")]
    public IActionResult AddStudent(StudentDto studentDto)
    {
        return Ok(_studentService.Create(studentDto));
    }
}