using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DietTracking.API.Data;

namespace DietTracking.API.Services
{
    public class AppointmentReminderService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public AppointmentReminderService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("--> Randevu Hatırlatma Servisi Başlatıldı.");

            // Uygulama çalıştığı sürece döngü devam eder
            while (!stoppingToken.IsCancellationRequested)
            {
                // SPAM KORUMASI:
                // Şu anki saat sabah 09:00 ile 09:59 arasında mı? 
                // Eğer değilse veritabanını boşuna yormaz ve mail atmaz.
                var currentHour = DateTime.Now.Hour;

                // Test etmek istersen buradaki 9'u şu anki saatine eşitle (örneğin 15).
                if (currentHour == 9)
                {
                    try
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                            // Yarınki tarihi al (Bugün + 1 gün)
                            var tomorrow = DateTime.UtcNow.AddDays(1).Date;

                            // Veritabanı Sorgusu
                            var upcomingAppointments = await context.Appointments
                                .Include(a => a.Client).ThenInclude(c => c.User)
                                // ÖNEMLİ DÜZELTME:
                                // StartDate nullable (DateTime?) olduğu için önce null değilse kontrolü yapıyoruz,
                                // sonra .Value.Date diyerek tarih kısmını alıyoruz.
                                .Where(a => a.StartDate != null && a.StartDate.Value.Date == tomorrow)
                                .ToListAsync(stoppingToken);

                            if (upcomingAppointments.Any())
                            {
                                Console.WriteLine($"--> Yarın için {upcomingAppointments.Count} adet randevu bulundu. Mailler gönderiliyor...");
                            }

                            foreach (var appt in upcomingAppointments)
                            {
                                var clientEmail = appt.Client?.User?.Email;
                                var clientName = appt.Client?.User?.FirstName;

                                // Tarihi güvenli şekilde string'e çevir
                                var appointmentDate = appt.StartDate.Value.ToString("dd.MM.yyyy HH:mm");

                                if (!string.IsNullOrEmpty(clientEmail))
                                {
                                    string subject = "Randevu Hatırlatması - Nutrivana";
                                    string body = $@"
                                        <div style='font-family: Arial, sans-serif; padding: 20px; border: 1px solid #e2e8f0; border-radius: 8px;'>
                                            <h2 style='color: #382aae;'>Merhaba {clientName},</h2>
                                            <p>Yarın (<strong>{appointmentDate}</strong>) tarihinde Dr. Expert ile randevunuz bulunmaktadır.</p>
                                            <p>Randevu saatinizden 10 dakika önce hazır bulunmanızı rica ederiz.</p>
                                            <br/>
                                            <hr style='border: 0; border-top: 1px solid #eee;' />
                                            <p style='color: #64748b; font-size: 12px;'>Sağlıklı günler dileriz,<br/><strong>Nutrivana Klinik Ekibi</strong></p>
                                        </div>";

                                    await emailService.SendEmailAsync(clientEmail, subject, body);
                                    Console.WriteLine($"--> Mail gönderildi: {clientEmail}");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"--> Mail servisinde hata oluştu: {ex.Message}");
                    }
                }

                // Döngü bittikten sonra servisi 1 saat uyut.
                // Böylece saat 09:05'te mail attıysa, bir sonraki kontrol 10:05'te olur 
                // ve saat koşulu (9) tutmayacağı için tekrar mail atmaz.
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}