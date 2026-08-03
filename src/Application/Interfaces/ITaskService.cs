using TasksApp.Application.DTOs;

namespace TasksApp.Application.Interfaces;

public interface ITaskService
{
    Task<IReadOnlyList<TaskDto>> GetAllTasksAsync(CancellationToken cancellationToken = default);
    Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto, CancellationToken cancellationToken = default);
    Task<TaskDto?> ToggleTaskStatusAsync(Guid id, CancellationToken cancellationToken = default);
}