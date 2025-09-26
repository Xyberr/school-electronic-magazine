using System.ComponentModel.DataAnnotations.Schema;

namespace school_electronic_magazine.Models;

public class Admin : User
{
    public string Email { get; set; }
    [NotMapped]
    public UserCredentials Credentials { get; set; }
    
    public Admin() { }
    
    public Admin(int id, string fullName, DateTime dateOfBirth, UserCredentials credential, string email, UserCredentials credentials) : base(id, fullName, dateOfBirth, credential)
    {
        Email = email;
        Credentials = credentials;
    }
}