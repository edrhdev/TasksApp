namespace TasksApp.BlazorWASM.Models;

public class TaskModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    public bool IsUpdating { get; set; }
}