using System.ComponentModel.DataAnnotations;
using school_electronic_magazine.Models.Base;

namespace school_electronic_magazine.Models;

public class RefreshToken : BaseEntity
{
    [Required]
    public required long UserId { get; set; }
    public User User { get; set; } = null!;
    
    [Required]
    public required string Token { get; set; } = null!;
    
    [Required]
    public required DateTime ExpiryDate { get; set; }
    
    [Required]
    public bool IsRevoked { get; set; }
}