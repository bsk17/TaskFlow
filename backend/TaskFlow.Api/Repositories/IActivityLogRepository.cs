using TaskFlow.Api.Models;

namespace TaskFlow.Api.Repositories
{
    /// <summary>
    /// Interface for the Activity Log repository.
    /// This interface defines methods for interacting with activity log entries in the database.
    /// </summary>
    public interface IActivityLogRepository
    {
        /// <summary>
        /// Asynchronously adds a new activity log entry to the database.
        /// </summary>
        Task AddASync(ActivityLog activityLog);
        
        /// <summary>
        /// Asynchronously retrieves a paginated list of activity logs based on the specified filters.
        /// </summary>
        /// <param name="entityType">The type of entity associated with the logs.</param>
        /// <param name="entityId">The ID of the entity associated with the logs.</param>
        /// <param name="userId">The ID of the user associated with the logs.</param>
        /// <param name="action">The action performed that generated the logs.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of logs per page.</param>
        /// <returns>A task that represents the asynchronous operation, containing a tuple with the logs and the total count of logs.</returns>
        Task<(IEnumerable<ActivityLog> Logs, long TotalCount)> GetPagedAsync(
        string entityType = null,
        string entityId = null,
        int? userId = null,
        string action = null,
        int page = 1,
        int pageSize = 50);
    }
}