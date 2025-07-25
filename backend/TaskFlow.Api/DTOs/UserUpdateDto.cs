
namespace TaskFlow.Api.DTOs
{
    /// <summary>
    /// Data Transfer Object for updating user information.
    /// Contains properties for Email, FullName, RoleId, and IsActive status.
    /// <remarks>
    /// The UserUpdateDto class is used to encapsulate the data required to update a user's
    /// information in the TaskFlow application. It is typically used in API requests to update user
    /// details without exposing the entire User entity.
    /// This DTO helps in maintaining a clean separation between the data layer and the API layer,
    /// ensuring that only the necessary fields are sent over the network.
    /// The properties in this DTO correspond to the fields that can be updated for a user.
    /// </summary>
    public class UserUpdateDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
