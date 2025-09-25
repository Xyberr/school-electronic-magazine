using school_electronic_magazine.Models;
using System.Collections.Generic;

namespace school_electronic_magazine.Repository.Student;


public interface IStudentRepository
{
    IEnumerable<Models.Student> GetAllStudents();
    Models.Student GetStudentById(int id);
    void Create(DTO.StudentDto studentDto);
    void Update(Models.Student student);
}