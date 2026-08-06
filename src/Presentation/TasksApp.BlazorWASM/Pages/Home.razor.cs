using TasksApp.BlazorWASM.Helpers;
using TasksApp.BlazorWASM.Models;

namespace TasksApp.BlazorWASM.Pages;

public partial class Home
{
    bool _isLoading = true;
    List<TaskModel> _tasks = [];

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;

        await LoadTasksAsync();
    }

    private void OnTaskDeleted(TaskModel deletedTask)
    {
        _tasks.Remove(deletedTask);
    }

    private async Task OnTaskToggled()
    {
        await InvokeAsync(StateHasChanged);
    }

    private void OnTaskCreated(TaskModel newTask)
    {
        _tasks.Add(newTask);
        _tasks = [.. _tasks.OrderByDescending(t => t.CreatedAt)];
    }

    private async Task LoadTasksAsync()
    {
        _isLoading = true;

        var result = await TaskService.GetAllTasksAsync();

        if (result.IsSuccess && result.Data is not null)
            _tasks = result.Data;
        else
            NotificationService.Notify(NotificationHelper.BuildFromApiResult(result, "Error", "Failed to load tasks."));

        _isLoading = false;

        await InvokeAsync(StateHasChanged);
    }
}
