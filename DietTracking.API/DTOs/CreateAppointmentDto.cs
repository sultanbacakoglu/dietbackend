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

        public int ClientId { get; set; }

        public int AppointmentStatusId { get; set; }

        public int AppointmentTypeId { get; set; }
    }
}