using TaskFlow.Api.DTOs;

namespace TaskFlow.Api.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationReadDto>> GetNotificationsAsync(int userId, bool unreadOnly = false);
        Task CreateNotificationAsync(int userId, string message);
        Task<bool> MarkAsReadAsync(int notifId, int userId);
    }
}