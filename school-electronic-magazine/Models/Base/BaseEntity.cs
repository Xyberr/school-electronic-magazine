using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_electronic_magazine.Models.Base;

public abstract class BaseEntity : ITimeStampable
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    //ITimeStampable
    [Required]
    public required DateTime CreationDate { get; set; }
    
    [Required]
    public required DateTime ModificationDate { get; set; }
}