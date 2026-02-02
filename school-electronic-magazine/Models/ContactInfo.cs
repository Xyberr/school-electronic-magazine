using System.ComponentModel.DataAnnotations;
using school_electronic_magazine.Models.Base;

namespace school_electronic_magazine.Models;

public class ContactInfo : BaseEntity
{
    [Required] 
    [MaxLength(50)]
    public required string Contact { get; set; } = null!;

    [Required] 
    public long ContactTypeId { get; set; }
    public ContactType ContactType { get; set; } = null!;
    
    [Required] 
    public long UserId { get; set; }
    public User User { get; set; } = null!;
}