using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Models;
using TaskFlow.Api.PagedResult;
using TaskFlow.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class AuditController : ControllerBase
{
    private readonly IActivityLogService _activityLogService;
    public AuditController(IActivityLogService activityLogService)
    {
        _activityLogService = activityLogService;
    }

    /// <summary>
    /// Retrieves a paginated list of activity logs based on the specified filters.
    /// </summary>
    /// <param name="entityType">The type of entity associated with the logs.</param>
    /// <param name="entityId">The ID of the entity associated with the logs.</param>
    /// <param name="userId">The ID of the user associated with the logs.</param>
    /// <param name="action">The action performed that generated the logs.</param>
    /// <param name="page">The page number for pagination.</param>
    /// <param name="pageSize">The number of logs per page.</param>
    /// <returns>A paginated result containing the activity logs.</returns> 
    [Authorize(Roles = "Admin")]
    [HttpGet("audit-logs")]
    public async Task<ActionResult<PagedResult<ActivityLogReadDto>>> GetAuditLogs(
        [FromQuery] string entityType = null,
        [FromQuery] string entityId = null,
        [FromQuery] int? userId = null,
        [FromQuery] string action = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50)
    {
        var logs = await _activityLogService.GetPagedAsync(entityType, entityId, userId, action, page, pageSize);
        return Ok(logs);
    }
}