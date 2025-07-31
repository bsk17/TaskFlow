using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;
        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a project by its ID.
        /// Includes related tasks in the result.
        /// Uses asynchronous operation to improve performance.
        /// This method is useful for retrieving detailed project information.
        /// It fetches the project along with its associated tasks, allowing for efficient data retrieval.  
        /// </summary>
        /// <param name="id">The ID of the project to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation, containing the project if found, or null if not found.</returns>
        public async Task<Project> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Gets all projects from the database.
        /// Uses asynchronous operation to improve performance.
        /// This method is useful for retrieving a list of all projects.
        /// It fetches all projects without any filtering, allowing for efficient data retrieval.   
        /// </summary>  
        /// <returns>A task that represents the asynchronous operation, containing a list of all projects.</returns>
        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        
        public async Task AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
        }
        
        
        public void Update(Project project)
        {
            _context.Projects.Update(project);
        }

        public void Delete(Project project)
        {
            _context.Projects.Remove(project);
        }

        
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}