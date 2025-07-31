namespace TaskFlow.Api.DTOs
{
    public class TaskCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public int? AssignedToUserId { get; set; }
        public DateTime? DueAt { get; set; }
        public int? ParentTaskId { get; set; }
    }
}
