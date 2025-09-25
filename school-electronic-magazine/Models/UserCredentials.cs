using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_electronic_magazine.Models;

public class UserCredentials
{

    [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    private int id;
    [Required]
    private string email;
    [Required]
    private string passwordHash;
    [Required]
    private string username;
    [Required]
    private string role;
    
    
    // FK и навигации к ролям
    public int? TeacherId { get; set; }
    public Teacher Teacher { get; set; }

    public int? StudentId { get; set; }
    public Student Student { get; set; }

    public int? AdminId { get; set; }
    public Admin Admin { get; set; }
    
    
    public UserCredentials(string email, string passwordHash, string username, string role, int id)
    {
        this.email = email;
        this.passwordHash = passwordHash;
        this.username = username;
        this.role = role;
        this.id = id;
    }

    public int Id
    {
        get => id; 
        set => id = value;
    }
    
    public string Role
    {
        get => role;
        set => role = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Username
    {
        get => username;
        set => username = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string PasswordHash
    {
        get => passwordHash;
        set => passwordHash = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Email
    {
        get => email;
        set => email = value ?? throw new ArgumentNullException(nameof(value));
    }
}