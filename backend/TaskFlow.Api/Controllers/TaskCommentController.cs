using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Services;

[ApiController]
[Route("api/tasks/{taskId}/comments")]
[Authorize]
public class TaskCommentController : ControllerBase
{
    private readonly ITaskCommentService _commentService;
    public TaskCommentController(ITaskCommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskCommentReadDto>>> GetComments(int taskId)
    {
        var comments = await _commentService.GetCommentsAsync(taskId);
        return Ok(comments);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddComment(int taskId, [FromBody] TaskCommentCreateDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        dto.TaskId = taskId;

        var added = await _commentService.AddCommentAsync(dto, userId);
        if (!added) return NotFound("Task not found or error saving comment.");

        return NoContent();
    }
}