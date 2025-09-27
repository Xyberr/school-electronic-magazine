using school_electronic_magazine.Data;
using school_electronic_magazine.Repository.GenericRepository;

namespace school_electronic_magazine.Services.Student;

public class StudentService : IStudentService
{
    private readonly IGenericRepository<Models.Student>  _repository;
    
    public StudentService(IGenericRepository<Models.Student> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Models.Student>> GetAllStudentsAsync()
    {
        return await _repository.GetAllAsync();
    }
    public async Task<Models.Student> GetStudentById(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null)
            throw new KeyNotFoundException($"Student with id {id} not found.");
    
        return student;
    }

    public async Task<string> Create(DTO.StudentDto studentDto)
    {

        var student = new Models.Student()
        {
            FullName = studentDto.FullName,
            DateOfBirth = studentDto.DateOfBirth,
            PhoneNumberParent = studentDto.PhoneNumberParent
        };
        
        try
        {
            await _repository.CreateAsync(student);
            return "Аккаунт создан";
        }
        catch (Exception ex)
        {
            return $"Аккаунт не удалось создать: {ex.Message}";
        }
    }
    

    public async Task<string> Update(int id, DTO.StudentDto studentDto)
    {
        try
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                return "Студент не найден";

            student.FullName = studentDto.FullName;
            student.DateOfBirth = studentDto.DateOfBirth;
            student.PhoneNumberParent = studentDto.PhoneNumberParent;

            await _repository.UpdateAsync(id, student);

            return $"Аккаунт {student.id} обновлен";
        }
        catch (Exception ex)
        {
            return "Аккаунт не удалось обновить!";
        }
    }

    public async Task<string> Delete(int id)
    {
        try
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                return "Такого аккаунта не существует";
            
            await _repository.DeleteAsync(id);

            return "Аккаунт успешно удалён";
        }
        catch (Exception e)
        {
            return "Не удалось удалить аккаунт";
        }
    }
}