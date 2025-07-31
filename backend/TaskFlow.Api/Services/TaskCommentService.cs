using AutoMapper;
using Newtonsoft.Json;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Models;
using TaskFlow.Api.Repositories;

namespace TaskFlow.Api.Services
{
    public class TaskCommentService : ITaskCommentService
    {
        private readonly ITaskCommentRepository _commentRepo;
        private readonly ITaskRepository _taskRepo;
        private readonly INotificationService _notificationService;
        private readonly IActivityLogService _activityLogService;
        private readonly IMapper _mapper;

        public TaskCommentService(
            ITaskCommentRepository commentRepo,
            ITaskRepository taskRepo,
            INotificationService notificationService,
            IActivityLogService activityLogService,
            IMapper mapper)
        {
            _commentRepo = commentRepo;
            _taskRepo = taskRepo;
            _notificationService = notificationService;
            _activityLogService = activityLogService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskCommentReadDto>> GetCommentsAsync(int taskId)
        {
            var comments = await _commentRepo.GetByTaskIdAsync(taskId);
            return _mapper.Map<IEnumerable<TaskCommentReadDto>>(comments);
        }

        public async Task<bool> AddCommentAsync(TaskCommentCreateDto dto, int userId)
        {
            var task = await _taskRepo.GetByIdAsync(dto.TaskId);
            if (task == null) return false;

            var comment = new TaskComment
            {
                TaskId = dto.TaskId,
                AuthorId = userId,
                Content = dto.Content
            };

            await _commentRepo.AddAsync(comment);
            var result = await _commentRepo.SaveChangesAsync();

            if (!result) return false;

            //Send Notification
            if (task.AssignedToUserId != null && task.AssignedToUserId != userId)
            {
                await _notificationService.CreateNotificationAsync(
                    task.AssignedToUserId.Value,
                    $"New comment on task: {task.Title}"
                );
            }

            //Audit log
            await _activityLogService.LogAsync(
                userId,
                "Task Commented",
                "Task",
                task.Id.ToString(),
                JsonConvert.SerializeObject(new { dto.Content })
            );

            return true;
        }
    }
}