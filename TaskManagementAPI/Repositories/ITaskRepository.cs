using TaskManagementAPI.Entities;

namespace TaskManagementAPI.Repositories;

public interface ITaskRepository
{
    Task<TaskItem?> GetTaskByIdAsync(int id);
    Task<IEnumerable<TaskItem>> GetTasksByOwnerAsync(string ownerId);
    Task<TaskItem> CreateTaskAsync(TaskItem taskItem);
    Task<bool> UpdateTaskAsync(TaskItem taskItem);
    Task<bool> DeleteTaskAsync(TaskItem task);
    Task<bool> SaveChangesAsync();
}
