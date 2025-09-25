namespace school_electronic_magazine.Models;

public class Subject
{
    private int Id { get; set; }
    private int IdTeacher { get; set; }
    
    private string name { get; set; }
    
    public Subject() { }
    
    public string Name 
    { 
        get => name; 
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Subject name cannot be null or empty", nameof(value));
            name = value;
        }
    }
    
    public Subject(string name, int idTeacher)
    {
        Name = name;
        IdTeacher = idTeacher;
    }
}
