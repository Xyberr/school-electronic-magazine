using school_electronic_magazine.Models;

namespace school_electronic_magazine.DTO;

public class StudentDto
{
    private string _fullName;
    private DateTime _dateOfBirth;
    private string phoneNumberParent;
    
    private StudentDto() { }

    public StudentDto(string fullName, DateTime dateOfBirth)
    {
        _fullName = fullName;
        _dateOfBirth = dateOfBirth;
    }

    public string PhoneNumberParent
    {
        get => phoneNumberParent;
        set => phoneNumberParent = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set => _dateOfBirth = value;
    }

    public string FullName
    {
        get => _fullName;
        set => _fullName = value ?? throw new ArgumentNullException(nameof(value));
    }
}