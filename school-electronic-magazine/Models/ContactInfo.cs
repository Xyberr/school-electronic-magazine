using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class ContactInfo : BaseEntity
{
    [Required] public required string Contact { get; set; } = null!;

    [Required] public required long ContactTypeId { get; set; }
    [Required] public required long UserId { get; set; }

    public ContactType ContactType { get; set; } = null!;
    public User User { get; set; } = null!;
}