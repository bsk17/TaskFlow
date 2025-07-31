using TaskFlow.Api.Models;

namespace TaskFlow.Api.Repositories
{
    public interface IPasswordResetTokenRepository
    {
        /// <summary>
        /// Adds a new PasswordResetToken to the repository.
        /// </summary>
        /// <param name="token">The PasswordResetToken to be added.</param>
        /// <returns>A Task that represents the asynchronous operation.</returns>
        Task AddASync(PasswordResetToken token);
        /// <summary>
        /// Retrieves a PasswordResetToken by its token string.
        /// </summary>
        /// <param name="token">The token string to search for.</param>
        /// <returns>A Task that represents the asynchronous operation, containing the PasswordResetToken if found
        Task<PasswordResetToken> GetByTokenAsync(string token);
        /// <summary>
        /// Retrieves a PasswordResetToken by its ID.
        /// </summary>
        /// <param name="id">The ID of the PasswordResetToken to retrieve.</param>
        /// <returns>A Task that represents the asynchronous operation, containing the PasswordResetToken if found
        void Update(PasswordResetToken token);
        /// <summary>
        /// Saves changes made to the repository.
        /// </summary>
        /// <returns>A Task that represents the asynchronous operation, returning true if changes were saved successfully, otherwise false.</returns>
        Task<bool> SaveChangesAsync();
    }
}