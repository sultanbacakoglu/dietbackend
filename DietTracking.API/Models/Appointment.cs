using System;

namespace DietTracking.API.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }      // PK
        public string? Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Notes { get; set; }

        // Foreign Keys
        public int? ClientId { get; set; }
        public int? AppointmentStatusId { get; set; }
        public int? AppointmentTypeId { get; set; }

        // Navigation Properties
        public Client? Client { get; set; }
        public AppointmentStatus? AppointmentStatus { get; set; }
        public AppointmentType? AppointmentType { get; set; }
    }
}