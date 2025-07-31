using TaskFlow.Api.Models;

namespace TaskFlow.Api.Repositories
{
    /// <summary>
    /// Interface for User Repository
    /// Provides methods for managing User entities in the TaskFlow application.
    /// <remarks>
    /// The IUserRepository interface defines the contract for user-related data operations.
    /// It includes methods for adding, deleting, retrieving, and updating User entities.
    /// Implementations of this interface should handle the actual data access logic, typically using a database context.
    /// The methods are asynchronous to support non-blocking operations, which is important for web applications.
    /// </remarks>
    /// <returns>
    /// An interface that defines methods for managing User entities.
    /// </summary>
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        void Delete(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUsernameAsync(string username);
        Task<bool> SaveChangesAsync();
        void Update(User user);
        Task<(IEnumerable<User> Users, int)> GetPagedAsync(string search = null, int? roleId = null, bool? isActive = null, int page = 1, int pageSize = 20); 
        Task<User> GetByEmailAsync(string email); 
    }
}