using TaskManagementAPI.DTOs;

namespace TaskManagementAPI.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskResponseDto>> GetAllTaskAsync(string ownerId);
    Task<TaskResponseDto?> GetTasksByOwnerAsync(int id, string ownerId);
    Task<TaskResponseDto> CreateTaskAsync(TaskRequestDto taskRequestDto, string ownerId);
    Task<bool> UpdateTaskAsync(int id, TaskUpdateDto taskUpdateDto, string ownerId);
    Task<bool> DeleteTaskAsync(int id, string ownerId);
}
