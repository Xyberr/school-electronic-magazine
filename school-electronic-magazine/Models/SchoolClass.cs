using System.ComponentModel.DataAnnotations;
using school_electronic_magazine.Models.Base;

namespace school_electronic_magazine.Models;

public class SchoolClass : BaseEntity
{
    [Required]
    public required string GroupLetter { get; set; } = null!;
    
    [Required]
    public required int ClassNumber { get; set; }
    
    public int EducationShift { get; set; }
    
    [Required]
    public required uint EnterDateForStudents { get; set; }

    [Required]
    public required long GroupId { get; set; }
    public Group Group { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = [];

}