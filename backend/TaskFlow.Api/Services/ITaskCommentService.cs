using TaskFlow.Api.DTOs;

namespace TaskFlow.Api.Services
{
    public interface ITaskCommentService
    {
        Task<IEnumerable<TaskCommentReadDto>> GetCommentsAsync(int taskId);
        Task<bool> AddCommentAsync(TaskCommentCreateDto dto, int userId);
    }
}