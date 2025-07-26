
using TaskFlow.Api.Models;
using TaskFlow.Api.PagedResult;
using TaskFlow.Api.Repositories;

namespace TaskFlow.Api.Services
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IActivityLogRepository _activityLogRepository;
        public ActivityLogService(IActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
        }

        /// <summary>
        /// Logs an activity in the system.
        /// </summary>
        /// <param name="userId">The ID of the user performing the action.</param>
        /// <param name="action">The action being logged.</param>
        /// <param name="entityTpe">The type of entity associated with the action.</param>
        /// <param name="entityId">The ID of the entity associated with the action.</param>
        /// <param name="details">Optional details about the action.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task LogAsync(int? userId, string action, string entityTpe, string entityId, string details = null)
        {
            var log = new ActivityLog
            {
                UserId = userId,
                Action = action,
                EntityType = entityTpe,
                EntityId = entityId,
                Details = details,
                Timestamp = DateTime.UtcNow
            };
            await _activityLogRepository.AddASync(log);
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
        public async Task<PagedResult<ActivityLog>> GetPagedAsync(
        string entityType = null,
        string entityId = null,
        int? userId = null,
        string action = null,
        int page = 1,
        int pageSize = 50)
        {
            var (logs, totalCount) = await _activityLogRepository.GetPagedAsync(entityType, entityId, userId, action, page, pageSize);

            return new PagedResult<ActivityLog>
            {
                Items = logs,
                TotalCount = (int)totalCount
            };
        }
    }
}