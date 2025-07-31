using AutoMapper;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Models;
using TaskFlow.Api.Repositories;

namespace TaskFlow.Api.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        public NotificationService(INotificationRepository notificationrepository, IMapper mapper)
        {
            _notificationRepository = notificationrepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificationReadDto>> GetNotificationsAsync(int userId, bool unreadOnly = false)
        {
            var notifs = unreadOnly ?
                await _notificationRepository.GetUnreadAsync(userId) :
                await _notificationRepository.GetAllAsync(userId);

            return _mapper.Map<IEnumerable<NotificationReadDto>>(notifs);
        }

        public async Task CreateNotificationAsync(int userId, string message)
        {
            var notif = new Notification { UserId = userId, Message = message };
            await _notificationRepository.AddAsync(notif);
            await _notificationRepository.SaveChangesAsync();
        }

        public async Task<bool> MarkAsReadAsync(int notifId, int userId)
        {
            await _notificationRepository.MarkAsReadAsync(notifId, userId);
            return await _notificationRepository.SaveChangesAsync();
        }
    }
}