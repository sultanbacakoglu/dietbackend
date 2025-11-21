using System.Collections.Generic;

namespace DietTracking.API.Models
{
    public class AppointmentType
    {
        public int AppointmentTypeId { get; set; }  // PK
        public string? Description { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
