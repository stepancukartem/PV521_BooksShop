using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PV521_BookssShop.Dtos;
using PV521_BookssShop.Services;

namespace PV521_BookssShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RoleDto>>> GetAll()
        {
            var roles = await _roleService.GetAll();

            return Ok(roles);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoleDto>> GetById(int id)
        {
            var role = await _roleService.GetById(id);

            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> Create(CreateRoleDto dto)
        {
            var role = await _roleService.Create(dto);

            if (role == null)
            {
                return Conflict("Роль з таким ім'ям вже існує.");
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = role.Id },
                role);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<RoleDto>> Update(
            int id,
            UpdateRoleDto dto)
        {
            var role = await _roleService.Update(id, dto);

            if (role == null)
            {
                return Conflict(
                    "Роль з таким ім'ям вже існує або роль не знайдена.");
            }

            return Ok(role);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _roleService.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}