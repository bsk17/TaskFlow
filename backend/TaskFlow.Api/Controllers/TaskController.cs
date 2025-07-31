using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Services;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskReadDto>> GetTask(int id)
    {
        var task = await _taskService.GetTaskAsync(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpGet("by-project/{projectId}")]
    public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetTasksByProject(int projectId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var tasks = await _taskService.GetTasksByProjectAsync(projectId, page, pageSize);
        return Ok(tasks);
    }

    [Authorize(Policy = "CanManageTasks")]
    [HttpPost]
    public async Task<ActionResult<TaskReadDto>> CreateTask(TaskCreateDto dto)
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdStr, out var userId))
        {
            return Unauthorized();
        }

        var task = await _taskService.CreateTaskAsync(dto, userId);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [Authorize(Policy = "CanManageTasks")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, TaskUpdateDto dto)
    {
        var updated = await _taskService.UpdateTaskAsync(id, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var deleted = await _taskService.DeleteTaskAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    [HttpGet("{id}/subtasks")]
    public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetSubTasks(int id)
    {
        var subtasks = await _taskService.GetSubTasksAsync(id);
        return Ok(subtasks);
    }

}