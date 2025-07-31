using AutoMapper;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Models;
using TaskFlow.Api.Repositories;

namespace TaskFlow.Api.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectReadDto> GetProjectAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            return project == null ? null : _mapper.Map<ProjectReadDto>(project);
        }

        public async Task<IEnumerable<ProjectReadDto>> GetProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectReadDto>>(projects);
        }

        public async Task<ProjectReadDto> CreateProjectAsync(ProjectCreateDto dto)
        {
            var project = _mapper.Map<Project>(dto);
            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();
            return _mapper.Map<ProjectReadDto>(project);
        }

        public async Task<bool> UpdateProjectAsync(int id, ProjectUpdateDto dto)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return false;

            _mapper.Map(dto, project);
            _projectRepository.Update(project);
            return await _projectRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return false;

            _projectRepository.Delete(project);
            return await _projectRepository.SaveChangesAsync();
        }
    }
}