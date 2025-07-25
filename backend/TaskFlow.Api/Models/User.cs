using System;

namespace TaskFlow.Api.Models
{
    /// <summary>
    /// Represents a user in the TaskFlow application.
    /// Contains properties for user details such as Id, Username, Email, PasswordHash, FullName, RoleId, Role, CreatedAt, and IsActive.
    /// <remarks>
    /// The User class is used to model the user entity in the application.
    /// It is typically used in conjunction with a database context to perform CRUD operations.
    /// The Id property is the primary key for the user, while RoleId is a foreign key that links to the Role entity.
    /// The CreatedAt property is set to the current UTC time by default when a new user is created.
    /// The IsActive property indicates whether the user is currently active in the system.
    /// </remarks>
    /// <returns>A User entity with properties for user details.</returns>
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; }
    }
}