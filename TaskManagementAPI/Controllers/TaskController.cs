using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementAPI.Services;
using TaskManagementAPI.DTOs;

namespace TaskManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
//[ApiExplorerSettings(GroupName = "v1")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        var tasks = await _taskService.GetAllTaskAsync(UserId);
        return Ok(tasks);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        var task = await _taskService.GetTasksByOwnerAsync(id, UserId);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskRequestDto taskRequestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var createdTask = await _taskService.CreateTaskAsync(taskRequestDto, UserId);
        return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskUpdateDto taskUpdateDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _taskService.UpdateTaskAsync(id, taskUpdateDto, UserId);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var result = await _taskService.DeleteTaskAsync(id, UserId);
        if (!result) return NotFound();
        return NoContent();
    }
}
