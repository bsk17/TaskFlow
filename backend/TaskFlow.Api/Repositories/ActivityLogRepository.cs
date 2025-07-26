
using MongoDB.Driver;
using TaskFlow.Api.Data;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Repositories
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly MongoDbContext _context;
        public ActivityLogRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task AddASync(ActivityLog activityLog)
        {
            await _context.ActivityLogs.InsertOneAsync(activityLog);
        }

        /// <summary>
        /// Asynchronously retrieves a paginated list of activity logs based on the specified filters.
        /// /// </summary>
        /// <param name="entityType">The type of entity associated with the logs.</param>
        /// <param name="entityId">The ID of the entity associated with the logs.</param>
        /// <param name="userId">The ID of the user associated with the logs.</param>
        /// <param name="action">The action performed that generated the logs.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of logs per page.</param>
        /// <returns>A task that represents the asynchronous operation, containing a tuple with the logs and the total count of logs.</returns>
        public async Task<(IEnumerable<ActivityLog>, long)> GetPagedAsync(
            string entityType = null,
            string entityId = null,
            int? userId = null,
            string action = null,
            int page = 1,
            int pageSize = 50
        )
        {
            var filterBuilder = Builders<ActivityLog>.Filter;
            var filter = filterBuilder.Empty;
            if (!string.IsNullOrEmpty(entityType))
            {
                filter &= filterBuilder.Eq(log => log.EntityType, entityType);
            }
            if (!string.IsNullOrEmpty(entityId))
            {
                filter &= filterBuilder.Eq(log => log.EntityId, entityId);
            }
            if (userId.HasValue)
            {
                filter &= filterBuilder.Eq(log => log.UserId, userId.Value);
            }
            if (!string.IsNullOrEmpty(action))
            {
                filter &= filterBuilder.Regex(log => log.Action, new MongoDB.Bson.BsonRegularExpression(action, "i"));
            }
            var totalCount = await _context.ActivityLogs.CountDocumentsAsync(filter);
            var logs = await _context.ActivityLogs
                .Find(filter)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return (logs, totalCount);
        }
    }
}