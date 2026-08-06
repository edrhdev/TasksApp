using Microsoft.AspNetCore.Mvc;
using TasksApp.Application.DTOs;
using TasksApp.Application.Interfaces;

namespace TasksApp.WebAPI.Controllers;

[Route("api/[controller]")]
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
    public async Task<IActionResult> Create(CreateTaskDto createTaskDto, CancellationToken cancellationToken)
    {
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
        return Ok(updatedTask);
    }

    /// <summary>
    /// Deletes a specific task by its ID.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await taskService.DeleteTaskAsync(id, cancellationToken);
        return NoContent();
    }
}
