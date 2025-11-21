using System;

namespace DietTracking.API.DTOs
{
    public class CreateAppointmentDto
    {
        public string? Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Notes { get; set; }

        //Foreign Keys

        // Randevuyu alan Client'ın ID'si
        public int ClientId { get; set; }

        // Başlangıçta randevu durumu (Örn: 1 = Waiting)
        public int AppointmentStatusId { get; set; }

        // Randevu türü (Örn: 1 = Online, 2 = Phone)
        public int AppointmentTypeId { get; set; }
    }
}