namespace school_electronic_magazine.Models;

public class RefreshToken
{
    public int Id { get; set; }
    public long UserId { get; set; }
    public string Token { get; set; } = null!;
    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; } = false;

    public User User { get; set; } = null!;
}