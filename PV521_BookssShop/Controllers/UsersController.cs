using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PV521_BookssShop.Dtos;
using PV521_BookssShop.Services;

namespace PV521_BookssShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(
            ChangePasswordDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized();

            var result = await _userService.ChangePassword(
                userId,
                dto);

            if (!result)
            {
                return BadRequest(
                    "Неправильний поточний пароль або користувача не знайдено.");
            }

            return Ok("Пароль успішно змінено.");
        }
    }
}