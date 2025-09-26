using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_electronic_magazine.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id;
    public string fullName;
    public DateTime dateOfBirth;

    public User() { }
    
    public User(int id, string fullName, DateTime dateOfBirth, UserCredentials credential)
    {
        this.id = id;
        this.fullName = fullName;
        this.dateOfBirth = dateOfBirth;
    }

    public DateTime DateOfBirth
    {
        get => dateOfBirth;
        set => dateOfBirth = value;
    }

    public string FullName
    {
        get => fullName;
        set => fullName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Id
    {
        get => id;
        set => id = value;
    }
}