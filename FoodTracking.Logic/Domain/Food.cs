using FoodTracking.Data.Dtos;

namespace FoodTracking.Logic.Domain
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Calories { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public decimal Protein { get; set; }
        public decimal Carbs { get; set; }
        public decimal Fats { get; set; }
        public string? Micronutrients { get; set; }
        public decimal ServingSize { get; set; }
        public string ServingUnit { get; set; } = string.Empty;

        public Food(int id, string name, string description, decimal calories, DateTime dateAdded, decimal protein, decimal carbs, decimal fats, string? micronutrients, decimal servingSize, string servingUnit)
        {
            Id = id;
            Name = name;
            Description = description;
            Calories = calories;
            DateAdded = dateAdded;
            Protein = protein;
            Carbs = carbs;
            Fats = fats;
            Micronutrients = micronutrients;
            ServingSize = servingSize;
            ServingUnit = servingUnit;
        }

        public Food(FoodDto foodDto)
        {
            Id = foodDto.Id;
            Name = foodDto.Name;
            Description = foodDto.Description;
            Calories = foodDto.Calories;
            DateAdded = foodDto.DateAdded;
            Protein = foodDto.Protein;
            Carbs = foodDto.Carbs;
            Fats = foodDto.Fats;
            Micronutrients = foodDto.Micronutrients;
            ServingSize = foodDto.ServingSize;
            ServingUnit = foodDto.ServingUnit;
        }

        public FoodDto GetFoodDto(Food food)
        {
            return new FoodDto() { Id = Id, Name = Name, Description = Description, Calories = Calories, DateAdded = DateAdded, Protein = Protein, Carbs = Carbs, Fats = Fats, Micronutrients = Micronutrients, ServingSize = ServingSize, ServingUnit = ServingUnit };
        }

    }
}
