using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DietTracking.API.Data;
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
            // 1. Kullanıcıyı veritabanında kullanıcı adına göre ara
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            // 2. Kullanıcı yoksa hata dön
            if (user == null)
            {
                return Unauthorized(new { Message = "Kullanıcı adı veya şifre hatalı." });
            }

            // 3. Şifre Kontrolü 
            if (user.PasswordHash != dto.Password)
            {
                return Unauthorized(new { Message = "Kullanıcı adı veya şifre hatalı." });
            }

            // 4. Rol Kontrolü 
            if (user.Role != "Expert")
            {
                return Unauthorized(new { Message = "Bu panele giriş yetkiniz yok." });
            }

            // 5. Giriş Başarılı
            return Ok(new
            {
                Message = "Giriş başarılı.",
                UserId = user.UserId,
                Username = user.Username,
                Role = user.Role,
                FullName = $"{user.FirstName} {user.LastName}"
            });
        }
    }
}