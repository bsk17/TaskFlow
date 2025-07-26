using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoSettings> settings)
        {
            var _client = new MongoClient(settings.Value.ConnectionString);
            _database = _client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<ActivityLog> ActivityLogs => 
            _database.GetCollection<ActivityLog>("ActivityLogs");
    }
}