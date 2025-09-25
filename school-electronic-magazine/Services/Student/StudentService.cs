using school_electronic_magazine.Repository.Student;

namespace school_electronic_magazine.Services.Student;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    
    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public IEnumerable<Models.Student> GetAllStudents()
    {
       return _studentRepository.GetAllStudents();
    }

    public Models.Student GetStudentById(int id)
    {
        var student = _studentRepository.GetStudentById(id);
        if (student == null)
            throw new KeyNotFoundException($"Student with id {id} not found.");
    
        return student;
    }

    public string Create(DTO.StudentDto studentDto)
    {
        try
        {
            _studentRepository.Create(studentDto);
            return "Аккаунт создан";
        }
        catch (Exception ex)
        {
            return $"Аккаунт не удалось создать: {ex.Message}";
        }
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