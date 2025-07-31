using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Repositories
{
    public class TaskCommentRepository : ITaskCommentRepository
    {
        private readonly AppDbContext _context;
        public TaskCommentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TaskComment>> GetByTaskIdAsync(int taskId)
        {
            return await _context.TaskComments
            .Include(c => c.Author)
            .Where(c => c.TaskId == taskId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
        }

        public async Task AddAsync(TaskComment comment)
        {
            await _context.TaskComments.AddAsync(comment);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}