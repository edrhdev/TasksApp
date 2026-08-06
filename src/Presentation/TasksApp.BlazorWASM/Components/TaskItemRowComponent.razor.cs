using Microsoft.AspNetCore.Components;
using Radzen;
using TasksApp.BlazorWASM.Helpers;
using TasksApp.BlazorWASM.Models;

namespace TasksApp.BlazorWASM.Components;

public partial class TaskItemRowComponent
{
    [Parameter, EditorRequired]
    public TaskModel TaskItem { get; set; }

    [Parameter, EditorRequired]
    public EventCallback<TaskModel> OnDeleteTask { get; set; }

    [Parameter, EditorRequired]
    public EventCallback OnTaskToggled { get; set; }

    private async Task ToggleTaskStatusAsync(bool newStatus)
    {
        TaskItem.IsUpdating = true;

        var result = await TaskService.ToggleTaskStatusAsync(TaskItem.Id);

        if (result.IsSuccess && result.Data is not null)
        {
            TaskItem.CompletedAt = result.Data.CompletedAt;
            TaskItem.IsCompleted = result.Data.IsCompleted;
            await OnTaskToggled.InvokeAsync();

            NotificationService.Notify(NotificationHelper.BuildFromApiResult(
                result,
                "Status Updated",
                $"Task '{TaskItem.Title}' marked as {(TaskItem.IsCompleted ? "completed" : "pending")}."));
        }
        else
        {
            TaskItem.IsCompleted = !newStatus;

            NotificationService.Notify(NotificationHelper.BuildFromApiResult(result,
                "Error Updating Status",
                "Failed to update the task status."));
        }

        TaskItem.IsUpdating = false;
        await InvokeAsync(StateHasChanged);
    }

    private async Task DeleteTaskAsync()
    {
        var confirm = await DialogService.Confirm($"Are you sure you want to delete the task '{TaskItem.Title}'?", "Confirm Deletion");

        if (confirm is null || confirm == false)
            return;

        TaskItem.IsUpdating = true;

        var result = await TaskService.DeleteTaskAsync(TaskItem.Id);

        if (result.IsSuccess)
        {
            await OnDeleteTask.InvokeAsync(TaskItem);
            NotificationService.Notify(NotificationHelper.BuildFromApiResult(result, "Task Deleted", $"The task '{TaskItem.Title}' was deleted successfully."));
        }
        else
        {
            TaskItem.IsUpdating = false;
            NotificationService.Notify(NotificationHelper.BuildFromApiResult(result, "Error Deleting Task", "Failed to delete the task."));
        }

        await InvokeAsync(StateHasChanged);
    }
}