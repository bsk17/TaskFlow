
namespace TaskFlow.Api.DTOs
{
    /// <summary>
    /// Data Transfer Object for reading user information.
    /// This DTO is used to transfer user data from the server to the client.
    /// It contains properties that are relevant for displaying user information without exposing sensitive data like PasswordHash.
    /// <remarks>
    /// The UserReadDto class is typically used in API responses to provide a simplified view of the User entity.
    /// It includes properties such as Id, Username, Email, FullName,
    /// RoleName, and CreatedAt, which are safe to expose to the client.
    /// The PasswordHash property is intentionally omitted to prevent exposing sensitive information.
    /// This DTO can be used in scenarios where user information needs to be displayed, such as
    /// user profiles, lists of users, or administrative interfaces.
    /// </remarks>
    /// </summary>
    public class UserReadDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
