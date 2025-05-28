namespace FoodTracking.Logic.Dtos
{
    public class FoodDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Calories { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public required decimal Protein { get; set; }
        public required decimal Carbs { get; set; }
        public required decimal Fats { get; set; }
        public string? Micronutrients { get; set; }
        public required decimal ServingSize { get; set; }
        public required string ServingUnit { get; set; }
    }
} 