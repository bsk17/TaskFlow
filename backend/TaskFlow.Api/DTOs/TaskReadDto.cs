using TaskFlow.Api.Helpers;

namespace TaskFlow.Api.DTOs
{
    public class TaskReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int CreatedByUserId { get; set; }
        public string CreatedByUsername { get; set; }
        public int? AssignedToUserId { get; set; }
        public string AssignedToUsername { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueAt { get; set; }
        public bool IsCompleted { get; set; }
        public int? ParentTaskId { get; set; }
        public Helpers.TaskStatus Status { get; set; } = Helpers.TaskStatus.Todo;

    }
}
