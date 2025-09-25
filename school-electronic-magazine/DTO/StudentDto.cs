namespace school_electronic_magazine.DTO;

public class StudentDto
{
    private string _fullName;
    private DateTime _dateOfBirth;
    
    private StudentDto() { }

    public StudentDto(string fullName, DateTime dateOfBirth)
    {
        _fullName = fullName;
        _dateOfBirth = dateOfBirth;
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