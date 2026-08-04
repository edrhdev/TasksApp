using Microsoft.AspNetCore.Mvc;
using TasksApp.Application.DTOs;
using TasksApp.Application.Interfaces;

namespace TasksApp.WebAPI.Controllers;

[Route("api/[controller]/[action]")]
[Produces("application/json")]
[ApiController]
public class TasksController(ITaskService taskService) : ControllerBase
{
    /// <summary>
    /// Gets a list of all tasks ordered by creation date.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var tasks = await taskService.GetAllTasksAsync(cancellationToken);
        return Ok(tasks);
    }

    /// <summary>
    /// Creates a new task.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto createTaskDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(createTaskDto.Title))
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid Payload",
                Detail = "Task title is required and cannot be empty."
            });
        }

        var createdTask = await taskService.CreateTaskAsync(createTaskDto, cancellationToken);

        return CreatedAtAction(nameof(Create), new { id = createdTask.Id }, createdTask);
    }

    /// <summary>
    /// Toggles the completion status of a specific task.
    /// </summary>
    [HttpPatch("{id:guid}/toggle")]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ToggleStatus(Guid id, CancellationToken cancellationToken)
    {
        var updatedTask = await taskService.ToggleTaskStatusAsync(id, cancellationToken);

        if (updatedTask is null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Task Not Found",
                Detail = $"No task was found with ID '{id}'."
            });
        }

        return Ok(updatedTask);
    }
}
