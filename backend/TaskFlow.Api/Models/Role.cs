namespace TaskFlow.Api.Models
{
    /// <summary>
    /// Represents a role in the TaskFlow application.
    /// Contains properties for role details such as Id, Name, and Description.
    /// <remarks>
    /// The Role class is used to model the role entity in the application.
    /// It is typically used in conjunction with a database context to perform CRUD operations.
    /// The Id property is the primary key for the role, while Name and Description provide additional information about the role.
    /// </remarks>
    /// <returns>A Role entity with properties for role details.</returns>
    /// </summary>
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}