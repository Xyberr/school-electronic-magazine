using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class Group : BaseEntity
{
    public long ClassId { get; set; }

    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<SchoolClass> SchoolClasses { get; set; } = new List<SchoolClass>();
}
