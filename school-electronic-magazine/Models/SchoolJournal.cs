using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class SchoolJournal : BaseEntity
{
    [Required] public required string SubjectName { get; set; }
    [Required] public required Grade Grade { get; set; }
    
    public User User { get; set; } = null!;
}