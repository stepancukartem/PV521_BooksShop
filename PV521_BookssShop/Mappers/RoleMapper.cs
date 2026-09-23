using PV521_BookssShop.Dtos;
using PV521_BooksShop.DAL.Entities;

namespace PV521_BookssShop.Mappers
{
    public static class RoleMapper
    {
        public static RoleDto ToDto(this Role role)
        {
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static Role ToEntity(this CreateRoleDto dto)
        {
            return new Role
            {
                Name = dto.Name
            };
        }

        public static void UpdateEntity(
            this Role role,
            UpdateRoleDto dto)
        {
            role.Name = dto.Name;
        }
    }
}