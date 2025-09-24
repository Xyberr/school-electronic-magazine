namespace school_electronic_magazine.Models;

public class User : BaseEntity
{
    private string _name;
    private int _age;
    private string _role;
    
    protected User() { }

    public User(string name, int age, string role)
    {
        _name = name;
        _age = age;
        _role = role;
    }

    public string Role
    {
        get => _role;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Role cannot be null or empty"); // Проверка на пустую роль
            _role = value;
        }
    }

    public int Age
    {
        get => _age;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Age cannot be negative."); // Проверка на отрицательный возраст
            _age = value;
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentOutOfRangeException(nameof(value), value, "Name cannot be empty or whitespace."); // Проверка на пустое имя
            _name = value;
        }
    }
}