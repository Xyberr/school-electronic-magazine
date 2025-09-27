namespace school_electronic_magazine.Models;

public class Homework
{
    public int Id { get; set; }
    public int AssignedToUserId { get; set; }
    public string HomeworkText { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public DateTime Deadline { get; set; }
    
    public Homework() { }
    
    public Homework(int assignedToUserId, string homeworkText, DateTime created, DateTime deadline)
    {
        AssignedToUserId = assignedToUserId;
        HomeworkText = homeworkText ?? throw new ArgumentNullException(nameof(homeworkText));
        Created = created;
        Deadline = deadline;
    }

    public string HomeworkTextChecked
    {
        get => HomeworkText;
        set => HomeworkText = value ?? throw new ArgumentNullException(nameof(value));
    }
}