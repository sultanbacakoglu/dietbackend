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
            // Randevu Modelini Oluşturma
            var appointment = new Appointment
            {
                Title = dto.Title,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Notes = dto.Notes,

                // Foreign Key Atama
                ClientId = dto.ClientId,
                AppointmentStatusId = dto.AppointmentStatusId,
                AppointmentTypeId = dto.AppointmentTypeId
            };

            // Veritabanına Kaydetme
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Başarılı Yanıt Döndürme 
            return Ok(appointment);
        }

        // 2. GET: Tüm Randevuları Listeleme
        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            // Randevuları ilgili Client, Status ve Type bilgileriyle birlikte çek
            var appointments = await _context.Appointments
                .Include(a => a.Client)
                    .ThenInclude(c => c.User)
                .Include(a => a.AppointmentStatus)
                .Include(a => a.AppointmentType)
                .ToListAsync();

            // DTO'ya dönüştürme ve veri aktarımı (AppointmentResponseDto kullanılarak)
            var response = appointments.Select(a => new AppointmentResponseDto
            {
                AppointmentId = a.AppointmentId,
                Title = a.Title,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                Notes = a.Notes,

                // İlişkili verilerin atanması
                ClientId = a.ClientId ?? 0,
                ClientUsername = a.Client?.User?.Username,
                StatusDescription = a.AppointmentStatus?.Description,
                TypeDescription = a.AppointmentType?.Description
            }).ToList();

            return Ok(response);
        }
    }
}