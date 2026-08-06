using TasksApp.BlazorWASM.Models;

namespace TasksApp.BlazorWASM.Services;

public interface ITaskApiClient
{
    Task<ApiResult<List<TaskModel>>> GetAllTasksAsync(CancellationToken cancellationToken = default);
    Task<ApiResult<TaskModel>> CreateTaskAsync(CreateTaskModel createTaskModel, CancellationToken cancellationToken = default);
    Task<ApiResult<TaskModel>> ToggleTaskStatusAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApiResult> DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default);
}
