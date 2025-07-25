using TaskFlow.Api.Data;
using TaskFlow.Api.Models;

public static class DbSeeder
{
    public static void SeedRoles(AppDbContext context)
    {
        // Only insert if table is empty (or use Any() for individual seeds)
        if (!context.Roles.Any())
        {
            context.Roles.AddRange(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" }
            );
            context.SaveChanges();
        }
    }
}