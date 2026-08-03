using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TasksApp.Domain.Interfaces;
using TasksApp.Infrastructure.Persistence;
using TasksApp.Infrastructure.Repositories;

namespace TasksApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        // TODO: SQLite should be replaced with a more robust database provider for production use, such as SQL Server or PostgreSQL.
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

        services.AddScoped<ITaskRepository, TaskRepository>();

        return services;
    }
}
