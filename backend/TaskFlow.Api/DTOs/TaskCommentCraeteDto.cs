namespace TaskFlow.Api.DTOs
{
    public class TaskCommentCreateDto
    {
        public int TaskId { get; set; }
        public string Content { get; set; }
    }
}