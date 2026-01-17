using DietTracking.API.Data;
using DietTracking.API.Models;
using Microsoft.EntityFrameworkCore;
// YENİ EKLENEN: Servislerin namespace'ini eklemeyi unutma
using DietTracking.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Servisleri ekle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- YENİ EKLENENLER BAŞLANGIÇ ---
// 1. Email Servisi (Mail göndermek için)
builder.Services.AddScoped<IEmailService, EmailService>();

// 2. Randevu Hatırlatıcı (Arka plan görevi - Saatte bir çalışır)
builder.Services.AddHostedService<AppointmentReminderService>();
// --- YENİ EKLENENLER BİTİŞ ---

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

// CORS 
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();
app.MapGet("/health", () => Results.Ok("API is running!"));

// Veritabanı ve Admin Kullanıcısı Oluşturma (Seeding)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        // Veritabanı yoksa oluşturur (Migration kullanıyorsan burayı context.Database.Migrate() yapabilirsin)
        context.Database.EnsureCreated();

        var adminUser = context.Users.FirstOrDefault(u => u.Username == "expert");

        if (adminUser == null)
        {
            // Yoksa oluştur
            context.Users.Add(new User
            {
                Username = "expert",
                PasswordHash = "123", // Şifre: 123
                Email = "expert@system.com",
                FirstName = "Sistem",
                LastName = "Yöneticisi",
                Role = "Expert",
                PhoneNumber = "5555555555"
            });
            context.SaveChanges();
            Console.WriteLine("--> 'expert' kullanıcısı (Şifre: 123) başarıyla oluşturuldu.");
        }
        else
        {
            Console.WriteLine("--> 'expert' kullanıcısı zaten mevcut.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("--> Veritabanı başlatılırken hata oluştu: " + ex.Message);
    }
}
// ----------------------------------------------------------

app.Run();