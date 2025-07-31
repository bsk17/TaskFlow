using TaskFlow.Api.DTOs;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Services
{
    public interface ITaskService
    {
        Task<TaskReadDto> GetTaskAsync(int id);
        Task<IEnumerable<TaskReadDto>> GetTasksByProjectAsync(int projectId, int page, int pageSize);
        Task<TaskReadDto> CreateTaskAsync(TaskCreateDto dto, int creatorUserId);
        Task<bool> UpdateTaskAsync(int id, TaskUpdateDto dto);
        Task<bool> DeleteTaskAsync(int id);
        Task<IEnumerable<TaskItem>> GetSubTasksAsync(int parentTaskId);

    }
}