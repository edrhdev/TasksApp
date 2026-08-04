using Microsoft.Extensions.DependencyInjection;
using TasksApp.Application.Interfaces;
using TasksApp.Application.Services;

namespace TasksApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITaskService, TaskService>();
        return services;
    }
}