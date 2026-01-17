using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DietTracking.API.Data;
using DietTracking.API.Models;
using DietTracking.API.DTOs;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace DietTracking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            var response = users.Select(u => new
            {
                u.UserId,
                u.Username,
                u.Email,
                u.FirstName,
                u.LastName,
                u.Role,
                u.PhoneNumber
            }).ToList();

            return Ok(response);
        }

        // PUT: api/users/change-password
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null)
                return NotFound(new { Message = "Kullanıcı bulunamadı." }); //JSON formatı

            // Eski şifre kontrolü
            if (user.PasswordHash != dto.CurrentPassword)
            {
                return BadRequest(new { Message = "Mevcut şifre hatalı." }); // JSON formatı
            }

            // Yeni şifreyi kaydet
            user.PasswordHash = dto.NewPassword;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Şifre başarıyla güncellendi." });
        }
    }
}