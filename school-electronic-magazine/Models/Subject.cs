using school_electronic_magazine.Models.Base;

namespace school_electronic_magazine.Models;

public class Subject : BaseEntity
{
    public string Name { get; set; } = null!;

    public ICollection<User> Teachers { get; set; } = [];
    public ICollection<Lesson> Lesson { get; set; } = [];
}