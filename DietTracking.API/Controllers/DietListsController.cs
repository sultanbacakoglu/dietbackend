using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DietTracking.API.Data;
using DietTracking.API.Models;
using DietTracking.API.DTOs;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace DietTracking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DietListsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DietListsController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Yeni Diyet Listesi Oluştur (Create)
        [HttpPost]
        public async Task<IActionResult> CreateDietList([FromBody] CreateDietListDto dto)
        {
            try
            {
                // Danışan kontrolü
                var clientExists = await _context.Clients.AnyAsync(c => c.ClientId == dto.ClientId);
                if (!clientExists)
                {
                    return BadRequest(new { Message = $"Hata: {dto.ClientId} ID'li bir danışan veritabanında bulunamadı." });
                }

                var dietList = new DietList
                {
                    ClientId = dto.ClientId,
                    Title = dto.Title,
                    Description = dto.Description,

                    // PostgreSQL için UTC ayarı
                    StartDate = DateTime.SpecifyKind(dto.StartDate, DateTimeKind.Utc),
                    EndDate = DateTime.SpecifyKind(dto.EndDate, DateTimeKind.Utc),

                    Details = new List<DietListDetail>()
                };

                if (dto.Details != null)
                {
                    foreach (var item in dto.Details)
                    {
                        dietList.Details.Add(new DietListDetail
                        {
                            Day = item.Day,
                            Meal = item.Meal,
                            Content = item.Content
                        });
                    }
                }

                _context.DietLists.Add(dietList);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Diyet listesi başarıyla oluşturuldu.", ListId = dietList.DietListId });
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += " | Detay: " + ex.InnerException.Message;
                }
                Console.WriteLine("Diyet Listesi Kayıt Hatası: " + errorMessage);
                return StatusCode(500, new { Message = "Sunucu Hatası", Error = errorMessage });
            }
        }

        // 2. Tüm Diyet Listelerini Getir (Tablo İçin)
        [HttpGet]
        public async Task<IActionResult> GetDietLists()
        {
            var lists = await _context.DietLists
                .Include(d => d.Client)
                    .ThenInclude(c => c.User)
                .OrderByDescending(d => d.StartDate)
                .ToListAsync();

            var response = lists.Select(d => new
            {
                d.DietListId,
                d.Title,
                d.Description,
                d.StartDate,
                d.EndDate,
                ClientName = d.Client?.User != null ? $"{d.Client.User.FirstName} {d.Client.User.LastName}" : "Bilinmiyor"
            });

            return Ok(response);
        }

        // 3. Tek Bir Listenin Detayını Getir (Detay Modalı İçin - KRİTİK DÜZELTME)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDietListById(int id)
        {
            var list = await _context.DietLists
                .Include(d => d.Details)
                .Include(d => d.Client)
                    .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(d => d.DietListId == id);

            if (list == null)
                return NotFound(new { Message = "Liste bulunamadı." });

            // Döngüsel referans hatasını önlemek için veriyi yeni bir objeye (DTO) map'liyoruz
            var response = new
            {
                list.DietListId,
                list.Title,
                list.Description,
                list.StartDate,
                list.EndDate,
                ClientName = list.Client?.User != null ? $"{list.Client.User.FirstName} {list.Client.User.LastName}" : "",
                // Details listesini temiz bir formata çeviriyoruz
                Details = list.Details.Select(d => new
                {
                    d.Day,
                    d.Meal,
                    d.Content
                }).ToList()
            };

            return Ok(response);
        }

        // 4. Belirli Bir Danışana Ait Listeleri Getir (Opsiyonel)
        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetDietListsByClient(int clientId)
        {
            var lists = await _context.DietLists
                .Include(d => d.Details)
                .Where(d => d.ClientId == clientId)
                .OrderByDescending(d => d.StartDate)
                .ToListAsync();

            if (lists == null || !lists.Any())
                return NotFound(new { Message = "Bu danışana ait diyet listesi bulunamadı." });

            // Burası için de basit bir dönüşüm yapmak iyi olur
            var response = lists.Select(l => new
            {
                l.DietListId,
                l.Title,
                l.StartDate,
                l.EndDate
            });

            return Ok(response);
        }
    }
}