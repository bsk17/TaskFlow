namespace TaskFlow.Api.DTOs
{
    public class ActivityLogReadDto
    {
        public string Id { get; set; }
        public int? UserId { get; set; }
        public string Action { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
    }

}