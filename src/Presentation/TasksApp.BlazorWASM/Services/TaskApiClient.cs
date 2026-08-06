using System.Net.Http.Json;
using TasksApp.BlazorWASM.Models;

namespace TasksApp.BlazorWASM.Services;

public class TaskApiClient(HttpClient _http, ILogger<TaskApiClient> _logger) : ITaskApiClient
{
    public async Task<ApiResult<List<TaskModel>>> GetAllTasksAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _http.GetAsync("api/tasks", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var tasks = await response.Content.ReadFromJsonAsync<List<TaskModel>>(cancellationToken: cancellationToken);
                return ApiResult<List<TaskModel>>.Success(tasks ?? []);
            }

            var problem = await ExtractProblemDetailsAsync(response, cancellationToken);

            return ApiResult<List<TaskModel>>.Failure(problem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected client error fetching tasks.");

            return ApiResult<List<TaskModel>>.Failure(new CustomProblemDetails
            {
                Title = "Unexpected Error",
                Detail = $"An unexpected error occurred while processing the request: {ex.Message}",
            });
        }
    }

    public async Task<ApiResult<TaskModel>> CreateTaskAsync(CreateTaskModel createTaskModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/tasks", createTaskModel, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var createdTask = await response.Content.ReadFromJsonAsync<TaskModel>(cancellationToken: cancellationToken);

                if (createdTask is not null)
                    return ApiResult<TaskModel>.Success(createdTask);
            }


            var problem = await ExtractProblemDetailsAsync(response, cancellationToken);

            return ApiResult<TaskModel>.Failure(problem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected client error creating task.");

            return ApiResult<TaskModel>.Failure(new CustomProblemDetails
            {
                Title = "Unexpected Error",
                Detail = $"An unexpected error occurred while creating the task: {ex.Message}",
            });
        }
    }

    public async Task<ApiResult<TaskModel>> ToggleTaskStatusAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _http.PatchAsync($"api/tasks/{id}/toggle", null, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var updatedTask = await response.Content.ReadFromJsonAsync<TaskModel>(cancellationToken: cancellationToken);

                if (updatedTask is not null)
                    return ApiResult<TaskModel>.Success(updatedTask);
            }

            var problem = await ExtractProblemDetailsAsync(response, cancellationToken);

            return ApiResult<TaskModel>.Failure(problem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected client error toggling task status for ID: {TaskId}", id);

            return ApiResult<TaskModel>.Failure(new CustomProblemDetails
            {
                Title = "Unexpected Error",
                Detail = $"An unexpected error occurred while updating the task status: {ex.Message}",
            });
        }
    }

    public async Task<ApiResult> DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/tasks/{id}", cancellationToken);

            if (response.IsSuccessStatusCode)
                return ApiResult.Success();

            var problem = await ExtractProblemDetailsAsync(response, cancellationToken);

            return ApiResult.Failure(problem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected client error deleting task for ID: {TaskId}", id);

            return ApiResult.Failure(new CustomProblemDetails
            {
                Title = "Unexpected Error",
                Detail = $"An unexpected error occurred while deleting the task: {ex.Message}",
            });
        }
    }

    private async Task<CustomProblemDetails> ExtractProblemDetailsAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        try
        {
            var problem = await response.Content.ReadFromJsonAsync<CustomProblemDetails>(cancellationToken: cancellationToken);

            if (problem is not null)
                return problem;
        }
        catch (Exception ex)
        {
            // Not valid JSON data, or not in the expected format. We'll handle this by logging the problem and returning a generic error message below.
            _logger.LogError(ex, "Error deserializing problem details from response. Status code: {StatusCode}", response.StatusCode);
        }

        return new CustomProblemDetails
        {
            Status = (int)response.StatusCode,
            Title = "HTTP Request Error",
            Detail = $"The server responded with status code {(int)response.StatusCode} ({response.StatusCode}: {response.ReasonPhrase})."
        };
    }
}
