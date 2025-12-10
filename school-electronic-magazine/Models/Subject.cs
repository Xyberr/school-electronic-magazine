using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class Subject : BaseEntity
{
    public string Name { get; set; } = null!;

    public ICollection<User> Teachers { get; set; } = new List<User>();
    public ICollection<Lesson> Lesson { get; set; } = new List<Lesson>();
}