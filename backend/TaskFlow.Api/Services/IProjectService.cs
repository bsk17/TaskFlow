using TaskFlow.Api.DTOs;

namespace TaskFlow.Api.Services
{
    public interface IProjectService
    {
        Task<ProjectReadDto> GetProjectAsync(int id);
        Task<IEnumerable<ProjectReadDto>> GetProjectsAsync();
        Task<ProjectReadDto> CreateProjectAsync(ProjectCreateDto dto);
        Task<bool> UpdateProjectAsync(int id, ProjectUpdateDto dto);
        Task<bool> DeleteProjectAsync(int id);
    }
}