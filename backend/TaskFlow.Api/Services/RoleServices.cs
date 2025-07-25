using AutoMapper;
using TaskFlow.Api.DTOs;
using TaskFlow.Api.Models;
using TaskFlow.Api.Repositories;

namespace TaskFlow.Api.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repo;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<RoleDto> GetRoleAsync(int id)
        {
            var role = await _repo.GetByIdAsync(id);
            return role == null ? null : _mapper.Map<RoleDto>(role);
        }

        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            var roles = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto> CreateRoleAsync(RoleDto dto)
        {
            var role = _mapper.Map<Role>(dto);
            await _repo.AddAsync(role);
            await _repo.SaveChangesAsync();
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<bool> UpdateRoleAsync(int id, RoleDto dto)
        {
            var role = await _repo.GetByIdAsync(id);
            if (role == null) return false;
            role.Name = dto.Name;
            _repo.Update(role);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _repo.GetByIdAsync(id);
            if (role == null) return false;
            _repo.Delete(role);
            return await _repo.SaveChangesAsync();
        }
    }
}