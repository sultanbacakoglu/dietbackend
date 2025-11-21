using System.Collections.Generic;

namespace DietTracking.API.Models
{
    public class AppointmentStatus
    {
        public int AppointmentStatusId { get; set; }  // PK
        public string? Description { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
