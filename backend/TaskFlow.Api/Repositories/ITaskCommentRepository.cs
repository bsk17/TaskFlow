using TaskFlow.Api.Models;

namespace TaskFlow.Api.Repositories
{
    public interface ITaskCommentRepository
    {
        Task<IEnumerable<TaskComment>> GetByTaskIdAsync(int taskId);
        Task AddAsync(TaskComment comment);
        Task<bool> SaveChangesAsync();
    }
}