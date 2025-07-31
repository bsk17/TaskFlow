using TaskFlow.Api.Models;

namespace TaskFlow.Api.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllAsync(int userId);
        Task<IEnumerable<Notification>> GetUnreadAsync(int userId);
        Task<Notification> GetByIdAsync(int id);
        Task AddAsync(Notification notif);
        Task MarkAsReadAsync(int id, int userId);
        Task<bool> SaveChangesAsync();
    }
}