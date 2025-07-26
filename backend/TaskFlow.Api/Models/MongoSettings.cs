namespace TaskFlow.Api.Models
{
    /// <summary>
    /// Represents the settings required to connect to a MongoDB database.
    /// This class contains properties for the connection string and database name.
    /// </summary>
    public class MongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}