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
        /// </summary>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        /// <summary>
        /// DbSet for Users
        /// Represents the collection of User entities in the database.
        /// This property allows for querying and saving instances of the User entity.
        /// <returns>A DbSet of User entities.</returns>
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// DbSet for Roles
        /// Represents the collection of Role entities in the database.
        /// This property allows for querying and saving instances of the Role entity.
        /// <returns>A DbSet of Role entities.</returns>
        /// </summary>
        public DbSet<Role> Roles { get; set; }
        
        /// <summary>
        /// DbSet for PasswordResetTokens
        /// Represents the collection of PasswordResetToken entities in the database.
        /// This property allows for querying and saving instances of the PasswordResetToken entity.
        /// <returns>A DbSet of PasswordResetToken entities.</returns>
        /// </summary>
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
    
        /// <summary>
        /// DbSet for Projects
        /// Represents the collection of Project entities in the database.
        /// This property allows for querying and saving instances of the Project entity.
        /// <returns>A DbSet of Project entities.</returns>
        /// </summary>
        public DbSet<Project> Projects { get; set; }

        /// <summary>
        /// DbSet for TaskItems
        /// Represents the collection of TaskItem entities in the database.
        /// This property allows for querying and saving instances of the TaskItem entity.
        /// <returns>A DbSet of TaskItem entities.</returns>
        /// </summary>
        public DbSet<TaskItem> TaskItems { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }


        /// <summary>
        /// Configures the model for the database context.
        /// This method is called by the framework to configure the model and relationships between entities.
        /// <param name="modelBuilder">The model builder used to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);

            modelBuilder.Entity<TaskItem>()
                .HasMany(t => t.SubTasks)
                .WithOne(t => t.ParentTask)
                .HasForeignKey(t => t.ParentTaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" }
            );
        }
    }
}