namespace TaskFlow.Api.DTOs
{
    public class UserProfileUpdateDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        // Add other fields if you wish (NEVER password here)
    }
}