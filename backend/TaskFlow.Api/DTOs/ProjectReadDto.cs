namespace TaskFlow.Api.DTOs
{
    public class ProjectReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        // Optionally, add number of tasks or related info if needed
    }
}
