using TaskFlow.Api.Models;

namespace TaskFlow.Api.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> GetByIdAsync(int id);
        Task<IEnumerable<Project>> GetAllAsync();
        Task AddAsync(Project project);
        void Update(Project project);
        void Delete(Project project);
        Task<bool> SaveChangesAsync();
    }
}