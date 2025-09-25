using System.ComponentModel.DataAnnotations.Schema;

namespace school_electronic_magazine.Models;

public class Student : User
{
    private int SchoolClassId { get; set; }
    private string PhoneNumberParent { get; set; }
    
    [NotMapped]
    public UserCredentials Credentials { get; set; }
    
    public Student() { }
    
    public Student(int id, string fullName, DateTime dateOfBirth, UserCredentials credential, int schoolClassId, string phoneNumberParent, UserCredentials credentials) : base(id, fullName, dateOfBirth, credential)
    {
        SchoolClassId = schoolClassId;
        PhoneNumberParent = phoneNumberParent;
        Credentials = credentials;
    }
}
