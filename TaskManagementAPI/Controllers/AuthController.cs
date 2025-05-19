using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementAPI.DTOs;

namespace TaskManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // Dummy user for demo purposes
    private readonly Dictionary<string, string> _users = new()
    {
        { "user1", "password1" },
        { "user2", "password2" }
    };

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(AuthRequestDto authRequestDto)
    {
        if (_users.TryGetValue(authRequestDto.Username, out var pw) && pw == authRequestDto.Password)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, authRequestDto.Username),
            new Claim(ClaimTypes.Name, authRequestDto.Username)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretkey_that_is_long_enough_123456!@#"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new AuthResponseDto(tokenString));
        }

        return Unauthorized();
    }

}
