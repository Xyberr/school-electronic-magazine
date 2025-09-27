using System.ComponentModel.DataAnnotations.Schema;

namespace school_electronic_magazine.Models;

public class Teacher : User
{
    
    public string email { get; set; }
    public string phoneNumber { get; set; }
    
    [NotMapped]
    public UserCredentials Credential { get; set; }
    public List<string> TeachingSubjects { get; set; }
    public List<string> HomeroomClasses { get; set; }

    Teacher() { }
    
    public Teacher(int id, string fullName, DateTime dateOfBirth, UserCredentials credential, string email, string phoneNumber, List<string> teachingSubjects, List<string> homeroomClasses) : base(id, fullName, dateOfBirth, credential)
    {
        this.email = email;
        this.phoneNumber = phoneNumber;
        Credential = credential;
        TeachingSubjects = teachingSubjects;
        HomeroomClasses = homeroomClasses;
    }
}
