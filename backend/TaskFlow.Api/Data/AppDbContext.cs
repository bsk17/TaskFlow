using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Data
{
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Constructor for AppDbContext
        /// Initializes a new instance of the AppDbContext class with the specified options.
        /// <param name="options">The options to be used by the DbContext.</param>
        /// <remarks>
        /// This constructor is used to configure the database context with the provided options,
        /// such as connection strings and other settings.
        /// It is typically called by the dependency injection framework when creating an instance of the context.
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        /// <summary>
        /// DbSet for Users
        /// Represents the collection of User entities in the database.
        /// This property allows for querying and saving instances of the User entity.
        /// <remarks>
        /// The Users DbSet is used to perform CRUD operations on User entities.
        /// It is mapped to the Users table in the database.
        /// The User entity contains properties such as Id, Username, Email, PasswordHash, FullName, RoleId, Role, CreatedAt, and IsActive.
        /// </remarks>
        /// <returns>A DbSet of User entities.</returns>
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// DbSet for Roles
        /// Represents the collection of Role entities in the database.
        /// This property allows for querying and saving instances of the Role entity.
        /// <remarks>
        /// The Roles DbSet is used to perform CRUD operations on Role entities.
        /// It is mapped to the Roles table in the database.
        /// The Role entity contains properties such as Id, Name, and Description.
        /// </remarks>
        /// <returns>A DbSet of Role entities.</returns>
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Configures the model for the database context.
        /// This method is called by the framework to configure the model and relationships between entities.
        /// <remarks>
        /// The OnModelCreating method is used to define relationships, constraints, and other configurations for the entities in the context.
        /// It is typically overridden to customize the model configuration.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId);
            
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" }
            );
        }
    }
}