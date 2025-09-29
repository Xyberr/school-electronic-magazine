using AutoMapper;
using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.Data;
using school_electronic_magazine.DTO;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repository.GenericRepository;

namespace school_electronic_magazine.Services.Student;

public class UserService(AppDbContext context, IGenericRepository<User> repository)
    : GenericRepository<User>(context), IUserService
{

    private readonly IGenericRepository<User> _repository = repository;

    public async Task<IEnumerable<User>> GetUserByName(string name)
    {
        return await context
            .Set<User>()
            .Where(x => x.Name == name)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetUserBySurname(string surname)
    {
        return await context
            .Set<User>()
            .Where(x => x.Surname == surname)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetUserByRole(string role)
    {
        return await context.Set<User>()
            .Where(x => x.Role == role)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetUserByClass(long classId)
    {
        return await context
            .Set<User>()
            .Where(x => x.ClassId == classId)
            .ToListAsync();
    }


    public async Task<User> CreateAsync(UserDto userDto)
    {
        var user = new User
        {
            Name = userDto.Name,
            Surname = userDto.Surname,
            DateOfBirth = userDto.DateOfBirth,
            LastOnline = DateTime.UtcNow,
            PasswordHash = userDto.PasswordHash,
            Login = userDto.Login,
            DateOfCreated = DateTime.UtcNow,
            ClassId = userDto.ClassId,
            Role = userDto.Role
        };

        return await _repository.CreateAsync(user);
    }

    public async Task<User> UpdateAsync(long id, UserDto userDto)
    {
        // Проверяем, что класс существует
        var schoolClass = await context.SchoolClasses
            .FirstOrDefaultAsync(c => c.Id == userDto.ClassId);
        
        var user = new User
        {
            Name = userDto.Name,
            Surname = userDto.Surname,
            DateOfBirth = userDto.DateOfBirth,
            LastOnline = DateTime.UtcNow,
            PasswordHash = userDto.PasswordHash,
            Login = userDto.Login,
            DateOfCreated = DateTime.UtcNow,
            ClassId = userDto.ClassId,
            SchoolClass = schoolClass,
            Role = userDto.Role
        };

        return await _repository.UpdateAsync(id, user);
    }

}


    //private readonly IGenericRepository<Student>  _repository;
    //
    // public StudentService(IGenericRepository<Models.Student> repository)
    // {
    //     _repository = repository;
    // }
    //
    // public async Task<IEnumerable<Models.Student>> GetAllStudentsAsync()
    // {
    //     return await _repository.GetAllAsync();
    // }
    // public async Task<Models.Student> GetStudentById(int id)
    // {
    //     var student = await _repository.GetByIdAsync(id);
    //     if (student == null)
    //         throw new KeyNotFoundException($"Student with id {id} not found.");
    //
    //     return student;
    // }
    //
    // public async Task<string> Create(DTO.StudentDto studentDto)
    // {
    //
    //     var student = new Models.Student()
    //     {
    //         FullName = studentDto.FullName,
    //         DateOfBirth = studentDto.DateOfBirth,
    //         //PhoneNumberParent = studentDto.PhoneNumberParent
    //     };
    //     
    //     try
    //     {
    //         await _repository.CreateAsync(student);
    //         return "Аккаунт создан";
    //     }
    //     catch (Exception ex)
    //     {
    //         return $"Аккаунт не удалось создать: {ex.Message}";
    //     }
    // }
    //
    //
    // public async Task<string> Update(int id, DTO.StudentDto studentDto)
    // {
    //     try
    //     {
    //         var student = await _repository.GetByIdAsync(id);
    //         if (student == null)
    //             return "Студент не найден";
    //
    //         student.FullName = studentDto.FullName;
    //         student.DateOfBirth = studentDto.DateOfBirth;
    //         student.PhoneNumberParent = studentDto.PhoneNumberParent;
    //
    //         await _repository.UpdateAsync(id, student);
    //
    //         return $"Аккаунт {student.id} обновлен";
    //     }
    //     catch (Exception ex)
    //     {
    //         return "Аккаунт не удалось обновить!";
    //     }
    // }
    //
    // public async Task<string> Delete(int id)
    // {
    //     try
    //     {
    //         var student = await _repository.GetByIdAsync(id);
    //         if (student == null)
    //             return "Такого аккаунта не существует";
    //         
    //         await _repository.DeleteAsync(id);
    //
    //         return "Аккаунт успешно удалён";
    //     }
    //     catch (Exception e)
    //     {
    //         return "Не удалось удалить аккаунт";
    //     }
    // }