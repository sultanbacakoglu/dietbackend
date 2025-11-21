using DietTracking.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Servisleri ekle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ----------------------------------------------------------
// 🚨 CORS GÜNCELLEMESİ: MOBİL GELİŞTİRME İÇİN GENİŞ İZİN
// ----------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()  // Her yerden gelen isteği kabul et (Mobil için kritik)
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// 2️⃣ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ----------------------------------------------------------
// 🚨 KRİTİK DÜZELTME: HTTPS YÖNLENDİRMESİNİ KAPAT
// ----------------------------------------------------------
// Android Emülatör SSL sertifikası hatası vermemesi için bunu kapatıyoruz.
// app.UseHttpsRedirection(); 
// ----------------------------------------------------------

app.UseRouting();

// CORS Middleware (UseRouting'den sonra, Auth'tan önce)
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();
app.MapGet("/health", () => Results.Ok("API is running!"));

app.Run();