using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class ContactType : BaseEntity
{
    [Required] 
    public required string Name { get; set; } = null!;
    
    public ICollection<ContactInfo>? ContactInfos { get; set; } 
}