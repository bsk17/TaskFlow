
using MongoDB.Bson;
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
            var filter = Builders<ActivityLog>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(entityType))
                filter &= Builders<ActivityLog>.Filter.Eq(x => x.EntityType, entityType);

            if (!string.IsNullOrWhiteSpace(entityId))
                filter &= Builders<ActivityLog>.Filter.Eq(x => x.EntityId, entityId);

            if (!string.IsNullOrWhiteSpace(action))
                filter &= Builders<ActivityLog>.Filter.Regex(x => x.Action, new BsonRegularExpression(action, "i"));

            if (userId.HasValue)
                filter &= Builders<ActivityLog>.Filter.Eq(x => x.UserId, userId.Value);

            var collection = _context.ActivityLogs;

            var total = await collection.CountDocumentsAsync(filter);

            var logs = await collection
                .Find(filter)
                .SortByDescending(l => l.Timestamp)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return (logs, total);
        }
    }
}