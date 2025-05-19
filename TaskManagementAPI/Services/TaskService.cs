using AutoMapper;
using TaskManagementAPI.Entities;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;
    public TaskService(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<TaskResponseDto>> GetAllTaskAsync(string ownerId)
    {
        var tasks = await _taskRepository.GetTasksByOwnerAsync(ownerId);
        return _mapper.Map<IEnumerable<TaskResponseDto>>(tasks);
    }
    public async Task<TaskResponseDto?> GetTasksByOwnerAsync(int id, string ownerId)
    {
        var task = await _taskRepository.GetTaskByIdAsync(id);
        if (task == null || task.OwnerId != ownerId) return null;
        return _mapper.Map<TaskResponseDto>(task);
    }
    public async Task<TaskResponseDto> CreateTaskAsync(TaskRequestDto taskRequestDto, string ownerId)
    {
        var taskItem = _mapper.Map<TaskItem>(taskRequestDto);
        taskItem.OwnerId = ownerId;
        var createdTask = await _taskRepository.CreateTaskAsync(taskItem);
        return _mapper.Map<TaskResponseDto>(createdTask);
    }
    public async Task<bool> UpdateTaskAsync(int id, TaskUpdateDto taskUpdateDto, string ownerId)
    {
        var task = await _taskRepository.GetTaskByIdAsync(id);
        if (task == null || task.OwnerId != ownerId) return false;
        _mapper.Map(taskUpdateDto, task);
        return await _taskRepository.UpdateTaskAsync(task);
    }
    public async Task<bool> DeleteTaskAsync(int id, string ownerId)
    {
        var task = await _taskRepository.GetTaskByIdAsync(id);
        if (task == null || task.OwnerId != ownerId) return false;
        
        return await _taskRepository.DeleteTaskAsync(task);
    }
}
