using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Models;
using TaskFlow.Api.Services;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _service;

    public RoleController(IRoleService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoleDto>> GetRole(int id)
    {
        var role = await _service.GetRoleAsync(id);
        if (role == null) return NotFound();
        return Ok(role);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
    {
        var roles = await _service.GetRolesAsync();
        return Ok(roles);
    }

    [HttpPost]
    public async Task<ActionResult<RoleDto>> CreateRole([FromBody] RoleDto dto)
    {
        var role = await _service.CreateRoleAsync(dto);
        return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleDto dto)
    {
        var updated = await _service.UpdateRoleAsync(id, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        var deleted = await _service.DeleteRoleAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}