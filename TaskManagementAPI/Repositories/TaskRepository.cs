using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Entities;

namespace TaskManagementAPI.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;
    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<TaskItem?> GetTaskByIdAsync(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }
    public async Task<IEnumerable<TaskItem>> GetTasksByOwnerAsync(string ownerId)
    {
        return await _context.Tasks.Where(t => t.OwnerId == ownerId).ToListAsync();
    }
    public async Task<TaskItem> CreateTaskAsync(TaskItem taskItem)
    {
        await _context.Tasks.AddAsync(taskItem);
        await SaveChangesAsync();
        return taskItem;
    }
    public async Task<bool> UpdateTaskAsync(TaskItem taskItem)
    {
        _context.Tasks.Update(taskItem);
        return await SaveChangesAsync();
    }
    public async Task<bool> DeleteTaskAsync(TaskItem task)
    {
        _context.Tasks.Remove(task);
        return await SaveChangesAsync();
    }
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
