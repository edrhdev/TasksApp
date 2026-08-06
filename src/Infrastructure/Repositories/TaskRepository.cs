using Microsoft.EntityFrameworkCore;
using TasksApp.Domain.Entities;
using TasksApp.Domain.Interfaces;
using TasksApp.Infrastructure.Persistence;

namespace TasksApp.Infrastructure.Repositories;

public class TaskRepository(AppDbContext context) : ITaskRepository
{
    public async Task<IReadOnlyList<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Tasks
            .AsNoTracking()
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Tasks.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task AddAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        await context.Tasks.AddAsync(task, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        context.Tasks.Update(task);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        context.Tasks.Remove(task);
        await context.SaveChangesAsync(cancellationToken);
    }
}