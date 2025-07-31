namespace TaskFlow.Api.DTOs
{
    public class TaskCommentReadDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public string AuthorUsername { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}