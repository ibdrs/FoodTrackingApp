namespace FoodTracking.Logic.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
} 