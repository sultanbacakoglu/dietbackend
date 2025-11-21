using System.Collections.Generic;

namespace DietTracking.API.Models
{
    public class Client
    {
        public int ClientId { get; set; }       // PK
        public int? UserId { get; set; }        // FK

        public User? User { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}