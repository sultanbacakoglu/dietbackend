
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DietTracking.API.Data;
using DietTracking.API.Models;
using DietTracking.API.DTOs;
using System.Threading.Tasks;
using System.Linq;

namespace DietTracking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/clients 
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                PasswordHash = dto.PasswordHash,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,

                PhoneNumber = dto.PhoneNumber,

                Role = "Client"
            };

            user.Client = new Client
            {
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { UserId = user.UserId, ClientId = user.Client.ClientId, Message = "Danışan başarıyla oluşturuldu." });
        }

        // GET: api/clients 
        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _context.Clients
                .Include(c => c.User)
                .Where(c => c.User != null && c.User.Role == "Client")
                .ToListAsync();

            var response = clients.Select(c => new ClientResponseDto
            {
                ClientId = c.ClientId,
                UserId = c.UserId ?? 0,

                FullName = $"{c.User?.FirstName} {c.User?.LastName}",
                Username = c.User?.Username,
                Email = c.User?.Email,

                PhoneNumber = c.User?.PhoneNumber ?? "N/A",

                LastAppointmentDate = "N/A"
            }).ToList();

            return Ok(response);
        }
    }
}