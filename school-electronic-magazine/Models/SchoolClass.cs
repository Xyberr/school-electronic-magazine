namespace school_electronic_magazine.Models;

public class SchoolClass
{
    private int Id { get; set; }
    private List<Student> Students { get; set; } = new();
    
    public SchoolClass() { }
    
    public SchoolClass(int id, List<Student> students, string teacher)
    {
        Id = id;
        Students = students ?? new List<Student>();
    }

    public List<Student> StudentsChecked
    {
        get => Students;
        set => Students = value ?? throw new ArgumentNullException(nameof(value));
    }
}
