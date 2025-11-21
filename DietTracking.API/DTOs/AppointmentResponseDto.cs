using System;

namespace DietTracking.API.DTOs
{
    public class AppointmentResponseDto
    {
        public int AppointmentId { get; set; }
        public string? Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Notes { get; set; }

        // İlişkili Bilgiler 
        public int ClientId { get; set; }
        public string? ClientUsername { get; set; }
        public string? StatusDescription { get; set; }
        public string? TypeDescription { get; set; }
    }
}