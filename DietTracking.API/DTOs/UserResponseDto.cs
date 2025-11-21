namespace DietTracking.API.DTOs
{
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Role { get; set; }

        public int? ClientId { get; set; }
        public bool IsClient { get; set; }
    }
}