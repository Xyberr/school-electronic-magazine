namespace school_electronic_magazine.Models;

public class Jwt
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
    public int ExpiresInMinutes { get; set; }
    public int RefreshTokenExpiresInMinutes { get; set; }
}