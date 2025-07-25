using TaskFlow.Api.DTOs;

namespace TaskFlow.Api.Services
{
    public interface IRoleService
    {
        Task<RoleDto> GetRoleAsync(int id);
        Task<IEnumerable<RoleDto>> GetRolesAsync();
        Task<RoleDto> CreateRoleAsync(RoleDto dto);
        Task<bool> UpdateRoleAsync(int id, RoleDto dto);
        Task<bool> DeleteRoleAsync(int id);
    }

}