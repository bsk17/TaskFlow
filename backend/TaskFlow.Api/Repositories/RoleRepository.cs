using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Repositories
{
    public class RoleRepository : IRoleRepository
    {
         private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Role> GetByIdAsync(int id) =>
            await _context.Roles.Include(r => r.Users).FirstOrDefaultAsync(r => r.Id == id);

        public async Task<Role> GetByNameAsync(string name) =>
            await _context.Roles.Include(r => r.Users).FirstOrDefaultAsync(r => r.Name == name);

        public async Task<IEnumerable<Role>> GetAllAsync() =>
            await _context.Roles.Include(r => r.Users).ToListAsync();

        public async Task AddAsync(Role role) => await _context.Roles.AddAsync(role);

        public void Update(Role role) => _context.Roles.Update(role);

        public void Delete(Role role) => _context.Roles.Remove(role);

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}
