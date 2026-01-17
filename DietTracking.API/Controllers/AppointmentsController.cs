using Microsoft.AspNetCore.Mvc;
using DietTracking.API.Data;
using DietTracking.API.Models;
using DietTracking.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace DietTracking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        // 1. POST: Yeni Randevu Oluşturma
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto dto)
        {
            var isConflict = await _context.Appointments
                .AnyAsync(a => a.StartDate < dto.EndDate && a.EndDate > dto.StartDate);

            if (isConflict)
            {
                return BadRequest("Seçilen saat aralığında zaten başka bir randevu mevcut.");
            }

            var appointment = new Appointment
            {
                Title = dto.Title,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Notes = dto.Notes,
                ClientId = dto.ClientId,
                AppointmentStatusId = dto.AppointmentStatusId,
                AppointmentTypeId = dto.AppointmentTypeId
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return Ok(appointment);
        }

        // 2. GET: Tüm Randevuları Listeleme
        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Client)
                    .ThenInclude(c => c.User)
                .Include(a => a.AppointmentStatus)
                .Include(a => a.AppointmentType)
                .ToListAsync();

            // Frontend için DTO 
            var response = appointments.Select(a => new AppointmentResponseDto
            {
                AppointmentId = a.AppointmentId,
                Title = a.Title,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                Notes = a.Notes,
                ClientId = a.ClientId ?? 0,
                ClientUsername = a.Client?.User?.Username,
                StatusDescription = a.AppointmentStatus?.Description,
                TypeDescription = a.AppointmentType?.Description
            }).ToList();

            return Ok(response);
        }
    }
}