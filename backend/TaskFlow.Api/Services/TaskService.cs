using AutoMapper;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Helpers;
using TaskFlow.Api.Models;
using TaskFlow.Api.Repositories;

namespace TaskFlow.Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskrepository;
        private readonly IUserRepository _userRepo;  // To validate creators/assignees if needed
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IUserRepository userRepo, INotificationService notificationService, IMapper mapper)
        {
            _taskrepository = taskRepository;
            _userRepo = userRepo;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<TaskReadDto> GetTaskAsync(int id)
        {
            var task = await _taskrepository.GetByIdAsync(id);
            return task == null ? null : _mapper.Map<TaskReadDto>(task);
        }

        public async Task<IEnumerable<TaskReadDto>> GetTasksByProjectAsync(int projectId, int page, int pageSize)
        {
            var tasks = await _taskrepository.GetTasksByProjectAsync(projectId, page, pageSize);
            return _mapper.Map<IEnumerable<TaskReadDto>>(tasks);
        }

        public async Task<TaskReadDto> CreateTaskAsync(TaskCreateDto dto, int creatorUserId)
        {
            var task = _mapper.Map<TaskItem>(dto);
            task.CreatedByUserId = creatorUserId;
            task.CreatedAt = DateTime.UtcNow;
            task.IsCompleted = false;

            await _taskrepository.AddAsync(task);
            await _taskrepository.SaveChangesAsync();

            if (dto.AssignedToUserId != null && dto.AssignedToUserId != creatorUserId)
    {
            await _notificationService.CreateNotificationAsync(
                dto.AssignedToUserId.Value,
                $"You have been assigned to task: {task.Title}"
            );
    }

            return _mapper.Map<TaskReadDto>(task);
        }

        public async Task<bool> UpdateTaskAsync(int id, TaskUpdateDto dto)
        {
            var task = await _taskrepository.GetByIdAsync(id);
            if (task == null) return false;
            if (!Utility.IsValidStatusTransition(task.Status, dto.Status)) {
                throw new InvalidOperationException("Invalid Task Status Transition");
            }

            _mapper.Map(dto, task);
            _taskrepository.Update(task);
            return await _taskrepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _taskrepository.GetByIdAsync(id);
            if (task == null) return false;

            _taskrepository.Delete(task);
            return await _taskrepository.SaveChangesAsync();
        }
        public async Task<IEnumerable<TaskItem>> GetSubTasksAsync(int parentTaskId)
        {
            return await _taskrepository.GetSubTasksAsync(parentTaskId);
        }
    }
}