namespace TaskFlow.Api.DTOs
{
    public class TaskUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AssignedToUserId { get; set; }
        public DateTime? DueAt { get; set; }
        public bool? IsCompleted { get; set; }
         public Helpers.TaskStatus Status { get; set; } = Helpers.TaskStatus.Todo;

    }
}
