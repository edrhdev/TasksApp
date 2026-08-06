using Microsoft.AspNetCore.Components;
using Radzen;
using TasksApp.BlazorWASM.Helpers;
using TasksApp.BlazorWASM.Models;

namespace TasksApp.BlazorWASM.Components;

public partial class TaskCreateFormComponent
{
    [Parameter]
    public bool IsBusy { get; set; }

    [Parameter]
    public EventCallback<TaskModel> OnTaskCreated { get; set; }

    private CreateTaskModel _newTask = new();

    private async Task CreateTaskAsync(CreateTaskModel newModel)
    {
        if (string.IsNullOrWhiteSpace(_newTask.Title)) return;

        IsBusy = true;

        var result = await TaskService.CreateTaskAsync(newModel);

        if (result.IsSuccess && result.Data is not null)
        {
            NotificationService.Notify(NotificationHelper.BuildFromApiResult(
                result,
                "Task Created",
                $"Added '{result.Data.Title}' correctly."));

            _newTask = new();
            await OnTaskCreated.InvokeAsync(result.Data);
        }
        else
        {
            NotificationService.Notify(NotificationHelper.BuildFromApiResult(
                result,
                "Error creating task",
                "Failed to create the task."));
        }

        IsBusy = false;
        await InvokeAsync(StateHasChanged);
    }
}