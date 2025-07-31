using TaskFlow.Api.Models;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;

namespace TaskFlow.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor for UserRepository
        /// Initializes a new instance of the UserRepository class with the specified AppDbContext.
        /// <returns>A UserRepository instance that can be used to manage User entities.</returns>
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new User to the database.
        /// This method asynchronously adds a User entity to the Users DbSet and returns the added entity.
        /// <returns>A Task that represents the asynchronous operation.
        /// The task result contains the added User entity.</returns>
        /// <param name="user">The User entity to be added.</param>
        /// </summary>
        public async Task<User> AddAsync(User user)
        {
            var entry = await _context.Users.AddAsync(user);
            return entry.Entity;
        }

        /// <summary>
        /// Deletes a User from the database.
        /// This method removes a User entity from the Users DbSet.
        /// It does not save changes to the database, so SaveChangesAsync must be called separately to persist the deletion.
        /// <returns>A Task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// The user parameter should not be null.
        /// If the user does not exist in the database, this method will not throw an exception,
        /// but the user will not be removed.
        /// It is the caller's responsibility to ensure that the user exists before calling this method.
        /// </remarks>      
        /// </summary>
        /// <param name="user"></param>
        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        /// <summary>
        /// Retrieves all Users from the database.
        /// This method asynchronously retrieves all User entities from the Users DbSet,
        /// including their associated Role entities.
        /// <returns>A Task that represents the asynchronous operation.
        /// The task result contains an IEnumerable of User entities.</returns> 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a User by its Id.
        /// This method asynchronously retrieves a User entity from the Users DbSet by its Id,
        /// including its associated Role entity.
        /// <returns>A Task that represents the asynchronous operation.
        /// The task result contains the User entity if found, or null if not found.</returns
        /// <remarks>
        /// If no User with the specified Id exists, this method will return null.
        /// It is the caller's responsibility to handle the case where the User is not found.
        /// </remarks>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Retrieves a User by its Username.
        /// This method asynchronously retrieves a User entity from the Users DbSet by its Username,
        /// including its associated Role entity.
        /// <returns>A Task that represents the asynchronous operation.
        /// The task result contains the User entity if found, or null if not found.</returns>
        /// <remarks>
        /// If no User with the specified Username exists, this method will return null.
        /// It is the caller's responsibility to handle the case where the User is not found.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// Saves changes to the database.
        /// This method asynchronously saves all changes made in the context to the database.
        /// <returns>A Task that represents the asynchronous operation.
        /// The task result contains a boolean indicating whether any changes were saved.</returns>
        /// <remarks>
        /// If no changes were made, this method will return false.
        /// If changes were made, it will return true.
        /// It is important to call this method after adding, updating, or deleting entities to persist
        /// those changes to the database.
        /// </remarks>
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        ///     Updates an existing User in the database.
        /// This method updates a User entity in the Users DbSet.
        /// It does not save changes to the database, so SaveChangesAsync must be called separately
        /// to persist the changes.
        /// <returns>A Task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// The user parameter should not be null.
        /// If the user does not exist in the database, this method will not throw an exception,
        /// but the user will not be updated.
        /// It is the caller's responsibility to ensure that the user exists before calling this method.
        /// </remarks>
        /// <returns></returns>
        /// </summary>
        /// <param name="user"></param>
        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        /// <summary>
        /// Retrieves a paginated list of Users with optional search functionality.
        /// This method asynchronously retrieves a paginated list of User entities from the Users DbSet,
        /// including their associated Role entities. It supports searching by Username or Email.
        /// <returns>A Task that represents the asynchronous operation.
        /// The task result contains a tuple with an IEnumerable of User entities and the total count of Users.</returns>
        /// <param name="search">Optional search term to filter Users by Username or Email.</param>
        /// <param name="page">The page number to retrieve (1-based).</param>
        /// <param name="pageSize">The number of Users to retrieve per page.</param>
        /// <remarks>
        /// If the search parameter is null or empty, all Users will be retrieved.
        /// If the page parameter is less than 1, it will default to 1.
        /// If the pageSize parameter is less than 1, it will default to 10.
        /// The total count of Users is also returned to allow for pagination in the UI.
        /// </remarks>
        /// </summary>
        public async Task<(IEnumerable<User>, int)> GetPagedAsync(string search = null, int? roleId = null, bool? isActive = null, int page = 1, int pageSize = 20)
        {
            var query = _context.Users.Include(u => u.Role).AsQueryable();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(u => u.Username.Contains(search) || u.Email.Contains(search));

            if (roleId.HasValue)
                query = query.Where(u => u.RoleId == roleId.Value);

            if (isActive.HasValue)
                query = query.Where(u => u.IsActive == isActive.Value);

            int total = await query.CountAsync();
            var users = await query
                        .OrderBy(u => u.Username) // Order by Username or any other field as needed
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
            return (users, total);
        }
        
        /// <summary>
        /// Retrieves a User by their email address.
        /// This method asynchronously retrieves a User entity from the Users DbSet by its email address.
        /// <returns>A Task that represents the asynchronous operation.
        /// The task result contains the User entity if found, or null if not found.</returns
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}

