using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Helpers;
using TaskFlow.Api.Models;
using TaskFlow.Api.PagedResult;
using TaskFlow.Api.Repositories;
using TaskFlow.Api.Services;

namespace TaskFlow.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IUserRepository _repository;
        public UserController(IUserServices userServices, IUserRepository userRepository)
        {
            _userServices = userServices;
            _repository = userRepository;
        }

        /// <summary>
        /// Get a user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDto>> GetUser(int id)
        {
            var user = await _userServices.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUsers()
        {
            var users = await _userServices.GetUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="userCreateDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserReadDto>> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            var user = await _userServices.CreateUserAsync(userCreateDto);
            if (user == null)
            {
                return BadRequest("User creation failed.");
            }
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userUpdateDto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            var updated = await _userServices.UpdateUserAsync(id, userUpdateDto);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Delete a user by ID
        /// This endpoint allows for the deletion of a user by their unique identifier.
        /// <remarks>        /// The DeleteUser method is used to remove a user from the system.
        /// It takes an integer ID as a parameter, which represents the unique identifier of the user to be deleted.
        /// If the user is successfully deleted, it returns a NoContent result.
        /// If the user with the specified ID does not exist, it returns a NotFound result.
        /// </remarks>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userServices.DeleteUserAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// User login endpoint
        /// This endpoint allows users to log in by providing their username and password.
        /// If the credentials are valid, it returns a JWT token for authentication.
        /// <remarks>     /// The Login method is used to authenticate a user.
        /// It takes a LoginDto object as a parameter, which contains the username and password.
        /// If the user is found and the password matches, it generates a JWT token using the JwtTokenGenerator.
        /// If the credentials are invalid, it returns an Unauthorized result.          
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto, [FromServices] JwtTokenGenerator jwtTokenGenerator)
        {
            var user = await _userServices.GetByUsernameAsync(loginDto.Username);
            if (user == null) return Unauthorized("Invalid username or password.");

            var userEntity = await _repository.GetByUsernameAsync(loginDto.Username);
            if (userEntity == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = jwtTokenGenerator.GenerateToken(userEntity);

            return Ok(new { token });
        }



        /// <summary>
        /// Get the current authenticated user
        /// This endpoint retrieves the details of the currently authenticated user based on the JWT token.
        /// <remarks>
        /// The GetCurrentUser method is used to retrieve the details of the currently authenticated user.
        /// It extracts the user ID from the JWT token claims and retrieves the user details using the IUserServices.
        /// If the user is found, it returns a UserReadDto with the user's  details.
        /// If the user is not authenticated or does not exist, it returns an appropriate error response.
        /// </remarks>
        /// <returns>
        /// A UserReadDto representing the currently authenticated user, or an error response if the user is not found or not authenticated.
        /// </returns>
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserReadDto>> GetCurrentUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                                ?? User.Claims.FirstOrDefault(c => c.Type == "sub");
            if (userIdClaim == null)
            {
                return Unauthorized("User not authenticated.");
            }

            int userId = int.Parse(userIdClaim.Value);
            var user = await _userServices.GetUserAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        /// <summary>
        /// Update the current user's profile
        /// This endpoint allows the authenticated user to update their profile information.
        /// <remarks>
        /// The UpdateProfile method is used to update the profile of the currently authenticated user.
        /// It extracts the user ID from the JWT token claims and updates the user's profile using the IUserServices.
        /// If the update is successful, it returns a NoContent result.     
        [HttpPut("me")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileUpdateDto userProfileUpdateDto)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                                ?? User.Claims.FirstOrDefault(c => c.Type == "sub");
            if (userIdClaim == null)
            {
                return Unauthorized("User not authenticated.");
            }

            int userId = int.Parse(userIdClaim.Value);
            var updated = await _userServices.UpdateProfileAsync(userId, userProfileUpdateDto);
            if (!updated)
            {
                return NotFound("User not found.");
            }
            return NoContent();
        }

        /// <summary>
        /// Change the current user's password
        /// This endpoint allows the authenticated user to change their password.
        /// <remarks>
        /// The ChangePassword method is used to change the password of the currently authenticated user.
        /// It extracts the user ID from the JWT token claims and changes the user's password using the IUserServices.
        /// If the password change is successful, it returns a NoContent result.
        /// If the user is not authenticated or the password change fails, it returns an appropriate error  response.
        /// </remarks>
        /// <returns>
        /// A NoContent result if the password change is successful, or an error response if the user is not authenticated or the password change fails.
        /// </returns>
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                                ?? User.Claims.FirstOrDefault(c => c.Type == "sub");
            if (userIdClaim == null)
            {
                return Unauthorized("User not authenticated.");
            }

            int userId = int.Parse(userIdClaim.Value);
            var changed = await _userServices.ChangePasswordAsync(userId, changePasswordDto);
            if (!changed)
            {
                return BadRequest("Password change failed.");
            }
            return NoContent();
        }

        /// <summary>
        /// Get a paginated list of users
        /// This endpoint retrieves a paginated list of users based on the search criteria and pagination parameters.
        /// <remarks>
        /// The GetUsers method is used to retrieve a paginated list of users.
        /// It accepts optional search criteria and pagination parameters (page number and page size).
        /// It returns a PagedResult<UserReadDto> containing the list of users and pagination information.
        /// If no users are found, it returns an empty list.
        /// </remarks>
        /// <returns>
        /// A PagedResult<UserReadDto> containing the list of users and pagination information.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("pagedusers")]
        public async Task<ActionResult<PagedResult<UserReadDto>>> PagedUsers(
            [FromQuery] string search = "",
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20
        )
        {
            var result = await _userServices.GetUsersPagedAsync(search, page, pageSize);
            return Ok(result);
        }

    }
}