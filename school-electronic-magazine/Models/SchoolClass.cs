using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_electronic_magazine.Models;


public class SchoolClass : BaseEntity
{
    [Required] public required long TeacherId { get; set; }
    [Required] public required string Name { get; set; }
    

    //public User Teacher { get; set; } = null!;
    public ICollection<User> Students { get; set; } = new List<User>(); // Ученики класса
}