using System.ComponentModel.DataAnnotations;
using school_electronic_magazine.Models.Base;

namespace school_electronic_magazine.Models;

public class ContactType : BaseEntity
{
    [Required] 
    [MaxLength(50)]
    public required string Name { get; set; } = null!;

    public ICollection<ContactInfo> ContactInfos { get; set; } = [];
}