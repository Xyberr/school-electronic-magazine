namespace school_electronic_magazine.Models;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
    public string ExpiresInMinutes { get; set; }
}