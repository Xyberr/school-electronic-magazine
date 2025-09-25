using Microsoft.AspNetCore.Http.HttpResults;
using school_electronic_magazine.Data;
using school_electronic_magazine.DTO;

namespace school_electronic_magazine.Repository.Student;

 public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;
    
    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Models.Student> GetAllStudents()
    {
       return _context.Students.ToList();
    }
    
    public Models.Student GetStudentById(int id)
    {
        return _context.Students.Find(id);
    }

    public void Create(DTO.StudentDto studentDto)
    {
        if (studentDto == null)
            throw new ArgumentNullException(nameof(studentDto));

        // Конвертация DTO в сущность
        var student = new Models.Student
        {
            FullName = studentDto.FullName,
            DateOfBirth =  studentDto.DateOfBirth,
        };

        _context.Students.Add(student);
        _context.SaveChanges();
    }

    public void Update(Models.Student student)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}