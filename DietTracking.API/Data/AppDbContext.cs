using Microsoft.EntityFrameworkCore;
using DietTracking.API.Models;

namespace DietTracking.API.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        public DbSet<AppointmentType> AppointmentTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("public");

            // USER - CLIENT (1-1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Client)
                .WithOne(c => c.User)
                .HasForeignKey<Client>(c => c.UserId);

            // CLIENT - APPOINTMENT (1-M)
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Appointments)
                .WithOne(a => a.Client)
                .HasForeignKey(a => a.ClientId);

            // APPOINTMENT STATUS - APPOINTMENT (1-M)
            modelBuilder.Entity<AppointmentStatus>()
                .HasMany(s => s.Appointments)
                .WithOne(a => a.AppointmentStatus)
                .HasForeignKey(a => a.AppointmentStatusId);

            // APPOINTMENT TYPE - APPOINTMENT (1-M)
            modelBuilder.Entity<AppointmentType>()
                .HasMany(t => t.Appointments)
                .WithOne(a => a.AppointmentType)
                .HasForeignKey(a => a.AppointmentTypeId);


            // APPOINTMENT STATUS SEED
            modelBuilder.Entity<AppointmentStatus>().HasData(
                new AppointmentStatus { AppointmentStatusId = 1, Description = "Waiting" },
                new AppointmentStatus { AppointmentStatusId = 2, Description = "Approved" },
                new AppointmentStatus { AppointmentStatusId = 3, Description = "Canceled" },
                new AppointmentStatus { AppointmentStatusId = 4, Description = "Completed" }
            );

            // APPOINTMENT TYPE SEED
            modelBuilder.Entity<AppointmentType>().HasData(
                new AppointmentType { AppointmentTypeId = 1, Description = "Online" },
                new AppointmentType { AppointmentTypeId = 2, Description = "Phone" },
                new AppointmentType { AppointmentTypeId = 3, Description = "F2F" }
            );

            modelBuilder.Entity<User>().HasData(
    new User
    {
        UserId = 99,
        Username = "expert",
        PasswordHash = "123",
        Email = "expert@diyet.com",
        FirstName = "Örnek",
        LastName = "Diyetisyen",
        Role = "Expert"
    });
        }

    }
}