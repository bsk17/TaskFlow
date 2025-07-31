using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Services;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectReadDto>> GetProject(int id)
    {
        var project = await _projectService.GetProjectAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectReadDto>>> GetProjects()
    {
        var projects = await _projectService.GetProjectsAsync();
        return Ok(projects);
    }

    [Authorize(Roles = "Admin,ProjectManager")]
    [HttpPost]
    public async Task<ActionResult<ProjectReadDto>> CreateProject(ProjectCreateDto dto)
    {
        var project = await _projectService.CreateProjectAsync(dto);
        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, ProjectUpdateDto dto)
    {
        var updated = await _projectService.UpdateProjectAsync(id, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [Authorize(Policy = "ProjectCreatorOrAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var deleted = await _projectService.DeleteProjectAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}