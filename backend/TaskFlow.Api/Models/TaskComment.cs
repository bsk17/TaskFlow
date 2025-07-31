namespace TaskFlow.Api.Models
{
    public class TaskComment
    {
        public int id { get; set; }
        public string Content { get; set; }
        public int TaskId { get; set; }
        public TaskItem Task { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}