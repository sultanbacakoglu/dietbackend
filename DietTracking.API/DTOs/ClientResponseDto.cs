namespace DietTracking.API.DTOs
{
    public class ClientResponseDto
    {
        public int ClientId { get; set; }
        public int UserId { get; set; }

        // Kullanıcıdan gelen temel bilgiler
        public string? FullName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
        public string? LastAppointmentDate { get; set; }
    }
}