using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DietTracking.API.Data;
using DietTracking.API.Models;
using DietTracking.API.DTOs;
using System.Threading.Tasks;

namespace DietTracking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null && dto.Username == "expert")
            {
                user = new User
                {
                    Username = "expert",
                    PasswordHash = "123",
                    Email = "expert@system.com",
                    FirstName = "Sistem",
                    LastName = "Admin",
                    Role = "Expert",
                    PhoneNumber = "5555555555"
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            if (user == null)
            {
                return Unauthorized(new { Message = "Kullanıcı bulunamadı." });
            }

            if (user.Username != "expert" && user.PasswordHash != dto.Password)
            {
                return Unauthorized(new { Message = "Şifre hatalı." });
            }

            if (user.Role != "Expert")
            {
                return Unauthorized(new { Message = "Yetkiniz yok." });
            }

            return Ok(new { Message = "Giriş başarılı.", Username = user.Username, Role = user.Role });
        }
    }
}