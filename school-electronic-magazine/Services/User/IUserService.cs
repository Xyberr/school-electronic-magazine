using school_electronic_magazine.DTO;
using school_electronic_magazine.Models;
using school_electronic_magazine.Repository.GenericRepository;

namespace school_electronic_magazine.Services.Student;

public interface IUserService : IGenericRepository<User>
{
    public Task<IEnumerable<User>> GetUserByName(string name);
    public Task<IEnumerable<User>> GetUserBySurname(string surname);
    public Task<IEnumerable<User>> GetUserByRole(string email);
    public Task<IEnumerable<User>> GetUserByClass(long classId);
    
    Task<User> CreateAsync(UserDto userDto);
    Task<User> UpdateAsync(long id, UserDto userDto);
}