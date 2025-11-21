using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DietTracking.API.Data;
using DietTracking.API.Models;
using DietTracking.API.DTOs; // UserResponseDto için gerekli
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace DietTracking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Bu satır sayesinde adres "api/users" olur
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
            // Veritabanındaki tüm kullanıcıları çek
            var users = await _context.Users.ToListAsync();

            // Entity'leri DTO'ya çevir (Şifreleri gizlemek için)
            // Eğer UserResponseDto dosyanız yoksa, geçici olarak anonim obje de döndürebiliriz ama DTO daha temizdir.
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
    }
}