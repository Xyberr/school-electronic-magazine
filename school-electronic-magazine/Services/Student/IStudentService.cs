namespace school_electronic_magazine.Services.Student;

public interface IStudentService
{
    IEnumerable<Models.Student> GetAllStudents();
    Models.Student GetStudentById(int id);
    string Create(DTO.StudentDto studentDto);
    void Update(Models.Student student);
    void Delete(int id);
}