using Microsoft.EntityFrameworkCore;
using TasksApp.Domain.Entities;

namespace TasksApp.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TaskItem table design with limitations and constraints
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Title)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(t => t.IsCompleted)
                  .IsRequired();

            entity.Property(t => t.CreatedAt)
                  .IsRequired();

            entity.Property(t => t.CompletedAt)
                  .IsRequired(false);
        });

        // Initial seed data for testing purposes
        modelBuilder.Entity<TaskItem>().HasData(
            new
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Title = "Build initial project structure",
                IsCompleted = true,
                CreatedAt = DateTime.Parse("2026-08-01T00:00:00Z"),
                CompletedAt = DateTime.Parse("2026-08-02T00:00:00Z")
            },
            new
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Title = "Implement Clean Architecture",
                IsCompleted = false,
                CreatedAt = DateTime.Parse("2026-08-01T01:00:00Z"),
                CompletedAt = (DateTime?)null
            },
            new
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Title = "Build Blazor WebAssembly frontend",
                IsCompleted = false,
                CreatedAt = DateTime.Parse("2026-08-01T02:00:00Z"),
                CompletedAt = (DateTime?)null
            },
            new
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Title = "Build React frontend",
                IsCompleted = false,
                CreatedAt = DateTime.Parse("2026-08-01T03:00:00Z"),
                CompletedAt = (DateTime?)null
            },
            new
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                Title = "Deliver the project & get the position",
                IsCompleted = false,
                CreatedAt = DateTime.Parse("2026-08-01T04:00:00Z"),
                CompletedAt = (DateTime?)null
            }
        );
    }
}
