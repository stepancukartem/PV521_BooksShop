using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PV521_BookssShop.Dtos;
using PV521_BookssShop.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PV521_BookssShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        private const string JwtKey =
            "PV521_BooksShop_SuperSecretKey_2026_123456789";

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userService.GetByEmailAsync(dto.Email);

            if (user == null)
                return Unauthorized("Неправильний email або пароль.");

            if (user.Password != dto.Password)
                return Unauthorized("Неправильний email або пароль.");

            if (user.Role == null)
                return Unauthorized("У користувача не визначена роль.");

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(
                    ClaimTypes.Email,
                    user.Email),

                new Claim(
                    ClaimTypes.Role,
                    user.Role.Name)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(JwtKey));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return Ok(new
            {
                token = tokenString,
                userId = user.Id,
                email = user.Email,
                role = user.Role.Name
            });
        }
    }
}