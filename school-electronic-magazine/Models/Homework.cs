namespace school_electronic_magazine.Models;

public class Homework
{
    private int Id { get; set; }
    private int AssignedToUserId { get; set; }
    private string HomeworkText { get; set; } = string.Empty;
    private DateTime Created { get; set; }
    private DateTime Deadline { get; set; }
    
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