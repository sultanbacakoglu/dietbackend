using System.Collections.Generic;

namespace DietTracking.API.Models
{
    public class User
    {
        public int UserId { get; set; }          // PK
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public Client? Client { get; set; }
    }
}