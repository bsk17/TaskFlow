
using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore.Metadata;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Models;
using TaskFlow.Api.PagedResult;
using TaskFlow.Api.Repositories;

namespace TaskFlow.Api.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        private readonly IActivityLogService _activityLogService;
        private readonly IMapper _mapper;
        public UserServices(IUserRepository userRepository, IPasswordResetTokenRepository passwordResetTokenRepository, IActivityLogService activityLogService, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordResetTokenRepository = passwordResetTokenRepository;
            _activityLogService = activityLogService;
        }

        /// <summary>
        /// Creates a new user in the system.
        /// Maps the UserCreateDto to a User entity, hashes the password, and saves it to the database.
        /// After saving, retrieves the newly created user by its Id and maps it
        /// to a UserReadDto for return.
        /// <returns>
        /// A UserReadDto representing the newly created user, or null if the creation failed.  
        public async Task<UserReadDto> CreateUserAsync(UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<User>(userCreateDto);
            user.PasswordHash = HashPassword(userCreateDto.Password);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            user = await _userRepository.GetByIdAsync(user.Id);
            return _mapper.Map<UserReadDto>(user);
        }

        /// <summary>
        /// Deletes a user by its Id.
        /// Retrieves the user from the repository, deletes it, and saves the changes.
        /// If the user does not exist, it returns false.
        /// <returns>
        /// A boolean indicating whether the deletion was successful.
        /// If the user was found and deleted, it returns true; otherwise, it returns false
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            _userRepository.Delete(user);
            return await _userRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a user by its Id.
        /// Maps the retrieved User entity to a UserReadDto for return.
        /// If the user does not exist, it returns null.
        /// <returns>
        /// A UserReadDto representing the user with the specified Id, or null if the user does not exist.      
        public async Task<UserReadDto> GetUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserReadDto>(user);
        }

        /// <summary>
        /// Retrieves all users from the system.
        /// Maps the collection of User entities to a collection of UserReadDto for return.
        /// <returns>
        /// A collection of UserReadDto representing all users in the system.
        /// If there are no users, it returns an empty collection.
        /// </returns>  
        public async Task<IEnumerable<UserReadDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        /// <summary>
        /// Updates an existing user by its Id.
        /// Maps the UserUpdateDto to the existing User entity, hashes the password if it has been updated,
        /// and saves the changes to the database.
        /// If the user does not exist, it returns false.
        /// <returns>
        /// A boolean indicating whether the update was successful.             
        public async Task<bool> UpdateUserAsync(int id, UserUpdateDto userUpdateDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            _mapper.Map(userUpdateDto, user);
            //Hash password here if it has been updated
            _userRepository.Update(user);
            return await _userRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Hashes a plain password using BCrypt.
        /// This method is used to securely hash a password before storing it in the database.      
        public string HashPassword(string plainPassword) => BCrypt.Net.BCrypt.HashPassword(plainPassword);

        /// <summary>
        /// Verifies a plain password against a hashed password.
        /// This method is used to check if a provided plain password matches the stored hashed password.
        /// </summary>
        /// <param name="plainPassword">The plain password to verify.</param>
        /// <param name="hashedPassword">The hashed password to compare against.</param>
        /// <returns>
        /// A boolean indicating whether the plain password matches the hashed password.
        /// Returns true if they match, otherwise false.
        /// </returns>
        public bool VerifyPassword(string plainPassword, string hashedPassword) => BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);

        /// <summary>
        /// Retrieves a user by username.
        /// Maps the retrieved User entity to a UserReadDto for return.
        /// If the user does not exist, it returns null.
        /// <returns>
        /// A UserReadDto representing the user with the specified username, or null if the user does not exist.
        /// </returns>
        public async Task<UserReadDto> GetByUsernameAsync(string username)
        {
            User user = await _userRepository.GetByUsernameAsync(username);
            return user == null ? null : _mapper.Map<UserReadDto>(user);
        }

        /// <summary>
        /// Updates the user profile.
        /// This method updates the user's profile information such as email and full name.
        /// It retrieves the user by their ID, updates the relevant fields, and saves the changes.
        /// If the user does not exist, it returns false.
        public async Task<bool> UpdateProfileAsync(int userId, UserProfileUpdateDto userProfileUpdateDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            user.Email = userProfileUpdateDto.Email;
            user.FullName = userProfileUpdateDto.FullName;

            _userRepository.Update(user);
            return await _userRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Changes the user's password.
        /// This method changes the user's password by hashing the new password and updating it in the database.
        /// It retrieves the user by their ID, updates the password, and saves the changes.         
        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            user.PasswordHash = HashPassword(changePasswordDto.NewPassword);
            _userRepository.Update(user);
            return await _userRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a paginated list of users based on search criteria.           
        public async Task<PagedResult<UserReadDto>> GetUsersPagedAsync(string search = null, int? roleId = null, bool? isActive = null, int page = 1, int pageSize = 20)
        {
            var (users, total) = await _userRepository.GetPagedAsync(search, roleId, isActive, page, pageSize);

            return new PagedResult<UserReadDto>
            {
                Items = _mapper.Map<IEnumerable<UserReadDto>>(users),
                TotalCount = total
            };
        }

        /// <summary>
        /// Updates the role of a user.
        /// This method updates the user's role by changing the RoleId of the user entity.
        /// It retrieves the user by their ID, updates the RoleId, and saves the changes.
        /// If the user does not exist, it returns false.
        public async Task<bool> UpdateUserRoleAsync(int userId, int newRoleId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            user.RoleId = newRoleId;
            _userRepository.Update(user);
            return await _userRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Sets the active status of a user.
        /// This method updates the IsActive property of the user entity.
        /// It retrieves the user by their ID, updates the IsActive status, and saves the changes.
        /// If the user does not exist, it returns false.
        public async Task<bool> SetUserActiveStatusAsync(int userId, bool isActive)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            user.IsActive = isActive;
            _userRepository.Update(user);
            return await _userRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Initiates a password reset process for a user by generating a reset token.
        /// This method retrieves the user by their email, generates a unique token, and saves it
        /// in the PasswordResetToken repository. It also logs the activity of the password reset request.
        /// If the user does not exist, it throws a KeyNotFoundException.
        /// </summary>
        /// <param name="email">The email of the user requesting the password reset.</param>
        /// <returns>
        /// A Task representing the asynchronous operation.
        /// </returns>
        public async Task InitiatePasswordResetAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var token = Guid.NewGuid().ToString("N");
            var passwordResetToken = new PasswordResetToken
            {
                UserId = user.Id,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                Used = false
            };

            await _passwordResetTokenRepository.AddASync(passwordResetToken);
            await _passwordResetTokenRepository.SaveChangesAsync();

            await _activityLogService.LogAsync(user.Id, "Password Reset Requested", "User", user.Id.ToString());

            // Here you would typically send the token to the user's email
            // For example: EmailService.SendPasswordResetEmail(user.Email, token);
        }

        /// <summary>
        /// Resets the user's password using a password reset token.
        /// This method retrieves the password reset token from the repository, checks if it is valid,
        /// and if so, updates the user's password with the new password provided.
        /// It also marks the token as used and logs the activity of the password reset.
        /// If the token is invalid or expired, it returns false.
        /// </summary>
        /// <param name="token">The password reset token provided by the user.</param>
        /// <param name="newPassword">The new password to set for the user.</param>
        /// <returns>
        /// A boolean indicating whether the password reset was successful.
        /// Returns true if the password was reset successfully, otherwise false.
        /// </returns>
        public async Task<bool> ResetPasswordAsync(string token, string newPassword)
        {
            var reset = await _passwordResetTokenRepository.GetByTokenAsync(token);
            if (reset == null || reset.Used || reset.ExpiresAt < DateTime.UtcNow)
            {
                return false; // Token is invalid or expired
            }

            var user = await _userRepository.GetByIdAsync(reset.UserId);
            if (user == null)
            {
                return false; // User not found
            }

            user.PasswordHash = HashPassword(newPassword);
            reset.Used = true;

            _userRepository.Update(user);
            _passwordResetTokenRepository.Update(reset);

            await _userRepository.SaveChangesAsync();
            await _passwordResetTokenRepository.SaveChangesAsync();

            await _activityLogService.LogAsync(user.Id, "Password Reset via Token Successful", "User", user.Id.ToString());

            return true; // Password reset successful
        }
    }
}