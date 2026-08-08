using TasksApp.Application.DTOs;
using TasksApp.Application.Interfaces;
using TasksApp.Domain.Entities;
using TasksApp.Domain.Exceptions;
using TasksApp.Domain.Interfaces;

namespace TasksApp.Application.Services;

public class TaskService(ITaskRepository repository) : ITaskService
{
    public async Task<IReadOnlyList<TaskDto>> GetAllTasksAsync(CancellationToken cancellationToken = default)
    {
        var tasks = await repository.GetAllAsync(cancellationToken);

        return [.. tasks.Select(MapToDto)];
    }

    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(createTaskDto.Title))
            throw new UserException("Task title is required and cannot be empty.");

        var task = new TaskItem(createTaskDto.Title);

        await repository.AddAsync(task, cancellationToken);

        return MapToDto(task);
    }

    public async Task<TaskDto?> ToggleTaskStatusAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new UserException($"Task with ID '{id}' not found.");

        task.ToggleStatus();

        await repository.UpdateAsync(task, cancellationToken);

        return MapToDto(task);
    }

    public async Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new UserException($"Task with ID '{id}' not found.");

        await repository.DeleteAsync(task, cancellationToken);
    }

    // TODO: Replace manual mapping with AutoMapper or similar library for better maintainability and scalability.
    private static TaskDto MapToDto(TaskItem task) =>
        new(task.Id,
            task.Title,
            task.IsCompleted,
            DateTime.SpecifyKind(task.CreatedAt, DateTimeKind.Utc),
            task.CompletedAt != null ?
            DateTime.SpecifyKind(task.CompletedAt.Value, DateTimeKind.Utc) : null);
}