using TaskFlow.Api.Models;

namespace TaskFlow.Api.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskItem> GetByIdAsync(int id);
        Task<IEnumerable<TaskItem>> GetTasksByProjectAsync(int projectId, int page, int pageSize);
        Task AddAsync(TaskItem task);
        void Update(TaskItem task);
        void Delete(TaskItem task);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<TaskItem>> GetSubTasksAsync(int parentTaskId);
}
}