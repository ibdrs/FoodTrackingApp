namespace FoodTracking.Logic.Dtos
{
    public class FoodDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? Calories { get; set; }
        public DateTime DateAdded { get; set; }
    }
} 