using TaskFlow.Api.DTOs;
using TaskFlow.Api.Models;
using TaskFlow.Api.PagedResult;

namespace TaskFlow.Api.Services
{
    /// <summary>
    /// Interface for the Activity Log service.
    /// This interface defines methods for logging activities in the application.
    /// </summary>
    public interface IActivityLogService
    {
        /// <summary>
        /// Logs an activity asynchronously.
        /// </summary>
        Task LogAsync(int? userId, string action, string entityTpe, string entityId, string details = null);
        
        /// <summary>
        /// Retrieves a paginated list of activity logs based on the specified filters.
        /// </summary>
        Task<PagedResult<ActivityLogReadDto>> GetPagedAsync(string entityType = null, string entityId = null, int? userId = null, string action = null, int page = 1, int pageSize = 50);
    }
}