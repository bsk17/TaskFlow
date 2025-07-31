using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Repositories
{
    public class PasswordResetTokenRepository : IPasswordResetTokenRepository
    {
        private readonly AppDbContext _context;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordResetTokenRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param
        public PasswordResetTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new PasswordResetToken to the repository.
        /// </summary>
        /// <param name="token">The PasswordResetToken to be added.</param>
        /// <returns>A Task that represents the asynchronous operation.</returns>
        public async Task AddASync(PasswordResetToken token)
        {
            await _context.PasswordResetTokens.AddAsync(token);
        }

        public async Task<PasswordResetToken> GetByTokenAsync(string token)
        {
            return await _context.PasswordResetTokens.FirstOrDefaultAsync(t => t.Token == token);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(PasswordResetToken token)
        {
            _context.PasswordResetTokens.Update(token);
        }
    }
}