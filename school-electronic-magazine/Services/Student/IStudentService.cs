namespace school_electronic_magazine.Services.Student;

public interface IStudentService
{
    Task<IEnumerable<Models.Student>> GetAllStudentsAsync();
    Task<Models.Student> GetStudentById(int id);
    Task<string> Create(DTO.StudentDto studentDto);
    Task<string> Update(int id, DTO.StudentDto studentDto);
    Task<string> Delete(int id);
}