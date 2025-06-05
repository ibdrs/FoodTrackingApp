namespace FoodTracking.Logic.Dtos
{
    public class FoodDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Calories { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbs { get; set; }
        public decimal Fats { get; set; }
        public string? Micronutrients { get; set; }
        public decimal ServingSize { get; set; }
        public string ServingUnit { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
} 