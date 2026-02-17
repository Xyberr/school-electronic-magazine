namespace school_electronic_magazine.Models.Base;

public interface ITimeStampable
{
    public DateTime CreationDate { get; set; }
    
    public DateTime ModificationDate { get; set; }
}