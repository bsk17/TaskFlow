using TaskFlow.Api.DTOs;
using TaskFlow.Api.PagedResult;

namespace TaskFlow.Api.Services
{
    /// <summary>
    /// Interface for User Services
    /// Provides methods for managing user-related operations such as retrieving, creating, updating, and deleting users.
    /// <remarks>
    /// This interface defines the contract for user services in the TaskFlow application.
    /// It includes methods for getting a single user by ID, retrieving all users, creating a new user, updating an existing user, and deleting a user.
    /// Implementations of this interface should handle the business logic and data access for user management.
    /// </remarks>
    /// <returns>
    /// A collection of methods for user management operations.
    /// Each method returns a Task representing the asynchronous operation, with specific return types for each operation.
    /// For example, GetUserAsync returns a UserReadDto, CreateUserAsync returns a UserReadDto, and UpdateUserAsync and DeleteUserAsync return a boolean indicating success or failure.
    /// </summary>
    public interface IUserServices
    {
        /// <summary>
        /// Retrieves a user by ID
        /// <remarks>
        /// This method fetches a user from the database based on the provided user ID.
        /// It returns a UserReadDto object containing the user's details.
        /// If the user is not found, it may return null or throw an exception depending on the implementation.
        /// </remarks>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A Task that represents the asynchronous operation, containing a UserReadDto with the user's details.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the user with the specified ID does not exist.</exception>
        /// <exception cref="ArgumentException">Thrown if the provided ID is invalid.</exception>
        /// <exception cref="Exception">Thrown if there is an error retrieving the user from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserReadDto> GetUserAsync(int id);

        /// <summary>
        /// Retrieves a user by username
        /// <remarks>               
        Task<UserReadDto> GetByUsernameAsync(string username);


        /// <summary>
        /// Retrieves all users
        /// <remarks>
        /// This method fetches all users from the database.
        /// It returns a collection of UserReadDto objects, each representing a user in the system.
        /// If there are no users, it may return an empty collection.
        /// </remarks>
        /// <returns>
        /// A Task that represents the asynchronous operation, containing an IEnumerable of UserReadDto with the details of all users.
        /// This collection can be used to display user information in a list or grid format.
        /// If there are no users, the collection will be empty.    
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UserReadDto>> GetUsersAsync();

        /// <summary>
        /// Creates a new user
        /// <remarks>
        /// This method creates a new user in the database based on the provided UserCreateDto.
        /// It performs validation on the input data and may throw exceptions if the data is invalid or if the user already exists.
        /// If the creation is successful, it returns a UserReadDto containing the details of the newly created user.
        /// </remarks>
        /// <returns>
        /// </summary>
        /// <param name="userCreateDto"></param>
        /// <returns></returns>
        Task<UserReadDto> CreateUserAsync(UserCreateDto userCreateDto);

        /// <summary>
        /// Updates an existing user
        /// <remarks>
        /// This method updates an existing user in the database based on the provided user ID and UserUpdateDto.
        /// It performs validation on the input data and may throw exceptions if the user does not exist or if the data is invalid.
        /// If the update is successful, it returns a boolean indicating success.
        /// If the user is not found, it may return false or throw an exception depending on the implementation.
        /// </remarks>
        /// <returns>
        /// A Task that represents the asynchronous operation, returning a boolean indicating whether the update was successful.
        /// If the user with the specified ID does not exist, it may return false or throw an exception.
        /// This method is typically used to modify user details such as username, email, password, and role.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userUpdateDto"></param>
        /// <returns></returns>
        Task<bool> UpdateUserAsync(int id, UserUpdateDto userUpdateDto);

        /// <summary>
        /// Deletes a user by ID
        /// <remarks>
        /// This method deletes a user from the database based on the provided user ID.
        /// It performs validation to ensure the user exists before attempting to delete.
        /// If the deletion is successful, it returns a boolean indicating success.
        /// If the user is not found, it may return false or throw an exception depending on the implementation.
        /// </remarks>
        /// <returns>
        /// A Task that represents the asynchronous operation, returning a boolean indicating whether the deletion was successful.
        /// If the user with the specified ID does not exist, it may return false or throw an exception.
        /// This method is typically used to remove a user from the system, ensuring that all related data is handled appropriately.
        /// </returns>
        /// <exception cref="KeyNotFoundException">Thrown if the user with the specified ID does not exist.</exception>
        /// <exception cref="ArgumentException">Thrown if the provided ID is invalid.</exception>
        /// <exception cref="Exception">Thrown if there is an error deleting the user from the database.
        /// This could include issues such as database connectivity problems or constraints preventing deletion.
        /// </exception>
        /// <remarks>
        /// This method is typically used in administrative functions to manage user accounts.
        /// It ensures that users can be removed from the system when they are no longer needed or when they violate terms of service.
        /// It is important to handle related data and ensure that the deletion does not leave orphaned records in the database.
        /// </remarks>
        /// <returns>
        /// A Task that represents the asynchronous operation, returning a boolean indicating whether the deletion was successful.
        /// If the user with the specified ID does not exist, it may return false or throw an exception.
        /// This method is typically used to remove a user from the system, ensuring that all related data is handled appropriately.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteUserAsync(int id);

        /// <summary>
        /// Updates the profile of a user
        /// <remarks>
        /// This method updates the profile information of a user based on the provided user ID and UserProfileUpdateDto.
        /// It performs validation on the input data and may throw exceptions if the user does not exist or if the data is invalid.
        /// If the update is successful, it returns a boolean indicating success.
        /// If the user is not found, it may return false or throw an exception depending on the implementation.
        /// </remarks>
        /// <returns>
        /// A Task that represents the asynchronous operation, returning a boolean indicating whether the profile update was successful.
        /// If the user with the specified ID does not exist, it may return false or throw an exception.
        /// This method is typically used to modify user profile details such as email, full name, and other personal information.
        /// </returns>
        Task<bool> UpdateProfileAsync(int userId, UserProfileUpdateDto userProfileUpdateDto);

        /// <summary>
        /// Changes the password of a user
        /// <returns>
        /// A Task that represents the asynchronous operation, returning a boolean indicating whether the password change was successful.
        /// If the user with the specified ID does not exist or if the old password does not match, it may return false or throw an exception.
        /// This method is typically used to allow users to update their passwords for security purposes.
        /// </returns>
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto);

        /// <summary>
        /// Retrieves a paginated list of users based on search criteria            
        Task<PagedResult<UserReadDto>> GetUsersPagedAsync(string search = null, int? roleId = null, bool? isActive = null, int page = 1, int pageSize = 20);

        /// <summary>
        /// Updates the role of a user
        Task<bool> UpdateUserRoleAsync(int userId, int newRoleId);
        
        /// <summary>
        /// Sets the active status of a user
        /// <remarks>
        /// This method sets the active status of a user based on the provided user ID and isActive flag.
        /// It performs validation to ensure the user exists before attempting to update the status.
        Task<bool> SetUserActiveStatusAsync(int userId, bool isActive);
    } 
}