using TaskFlow.Api.Helpers;

namespace TaskFlow.Api.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        public int? AssignedToUserId { get; set; }
        public User AssignedToUser { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueAt { get; set; }
        public bool IsCompleted { get; set; } = false;
        public int? ParentTaskId { get; set; }
        public TaskItem ParentTask { get; set; }

        public Helpers.TaskStatus Status { get; set; } = Helpers.TaskStatus.Todo;
        public ICollection<TaskItem> SubTasks { get; set; } = new List<TaskItem>();
    }

}
