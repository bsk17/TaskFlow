using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;
    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NotificationReadDto>>> GetMyNotifications([FromQuery] bool unreadOnly = false)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var notifs = await _notificationService.GetNotificationsAsync(userId, unreadOnly);
        return Ok(notifs);
    }

    [HttpPut("{id}/read")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var updated = await _notificationService.MarkAsReadAsync(id, userId);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpGet("unread-count")]
    public async Task<ActionResult<int>> GetUnreadCount()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var notifs = await _notificationService.GetNotificationsAsync(userId, unreadOnly: true);
        return Ok(notifs.Count());
    }
}