using Microsoft.EntityFrameworkCore;
using PV521_BookssShop.Dtos;
using PV521_BookssShop.Mappers;
using PV521_BooksShop.DAL.Repositories;

namespace PV521_BookssShop.Services
{
    public class RoleService
    {
        private readonly RoleRepository _roleRepository;

        public RoleService(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleDto>> GetAll()
        {
            var roles = await _roleRepository.Roles.ToListAsync();

            return roles
                .Select(r => r.ToDto())
                .ToList();
        }

        public async Task<RoleDto?> GetById(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);

            if (role == null)
                return null;

            return role.ToDto();
        }

        public async Task<RoleDto?> Create(CreateRoleDto dto)
        {
            var existingRole =
                await _roleRepository.GetByNameAsync(dto.Name);

            if (existingRole != null)
                return null;

            var role = dto.ToEntity();

            var created = await _roleRepository.CreateAsync(role);

            if (!created)
                return null;

            return role.ToDto();
        }

        public async Task<RoleDto?> Update(
            int id,
            UpdateRoleDto dto)
        {
            var role = await _roleRepository.GetByIdAsync(id);

            if (role == null)
                return null;

            var existingRole =
                await _roleRepository.GetByNameAsync(dto.Name);

            if (existingRole != null &&
                existingRole.Id != id)
            {
                return null;
            }

            role.UpdateEntity(dto);

            var updated = await _roleRepository.UpdateAsync(role);

            if (!updated)
                return null;

            return role.ToDto();
        }

        public async Task<bool> Delete(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);

            if (role == null)
                return false;

            return await _roleRepository.DeleteAsync(role);
        }
    }
}