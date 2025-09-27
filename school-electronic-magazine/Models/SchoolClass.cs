namespace school_electronic_magazine.Models;

public class SchoolClass
{
    private int id { get; set; }
    private List<Student> Students { get; set; } = new();
    
    public SchoolClass() { }
    
    public SchoolClass(int id, List<Student> students, string teacher)
    {
        this.id = id;
        Students = students ?? new List<Student>();
    }

    public List<Student> StudentsChecked
    {
        get => Students;
        set => Students = value ?? throw new ArgumentNullException(nameof(value));
    }
}
