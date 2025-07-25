
namespace TaskFlow.Api.DTOs
{
    /// <summary>
    /// Data Transfer Object for creating a new user.
    /// This DTO is used to encapsulate the data required to create a new user in the application.
    /// It includes properties for Username, Email, Password, FullName, and RoleId.
    /// <remarks>
    /// The UserCreateDto is typically used in API endpoints to receive user creation requests.
    /// It helps to validate and transfer the necessary data without exposing the entire User model.
    /// The Password property is intended to be hashed before being stored in the database.
    /// The RoleId property is used to associate the new user with a specific role in the application.
    /// </remarks>
    /// </summary>
   public class UserCreateDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
    } 
}
