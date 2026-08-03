namespace TasksApp.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    // EF Core requires a parameterless constructor for entity materialization
    private TaskItem() { }

    public TaskItem(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Task title cannot be empty.", nameof(title));
        }

        Id = Guid.NewGuid();
        Title = title.Trim();
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void ToggleStatus()
    {
        IsCompleted = !IsCompleted;
        CompletedAt = IsCompleted ? DateTime.UtcNow : null;
    }
}