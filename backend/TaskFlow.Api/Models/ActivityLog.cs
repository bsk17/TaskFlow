namespace TaskFlow.Api.Models
{
    /// <summary>
    /// Represents an activity log entry in the TaskFlow application.
    /// This class is used to log user activities such as actions performed, timestamps, and associated metadata.
    /// </summary>
    public class ActivityLog
    {
        public string Id { get; set; }
        public int? UserId { get; set; }
        public string Action { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}