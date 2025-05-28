using FoodTracking.Logic.Dtos;

namespace FoodTracking.Logic.Domain
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Calories { get; set; }
        public DateTime DateAdded { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbs { get; set; }
        public decimal Fats { get; set; }
        public string? Micronutrients { get; set; }
        public decimal ServingSize { get; set; }
        public string ServingUnit { get; set; } = string.Empty;

        public Food(int id, string name, string description, decimal calories, DateTime dateAdded)
        {
            Id = id;
            Name = name;
            Description = description;
            Calories = calories;
            DateAdded = dateAdded;
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
            return new FoodDto
            {
                Id = food.Id,
                Name = food.Name,
                Description = food.Description,
                Calories = food.Calories,
                DateAdded = food.DateAdded,
                Protein = food.Protein,
                Carbs = food.Carbs,
                Fats = food.Fats,
                Micronutrients = food.Micronutrients,
                ServingSize = food.ServingSize,
                ServingUnit = food.ServingUnit
            };
        }

        public static Food FromDto(FoodDto dto)
        {
            return new Food
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Calories = dto.Calories,
                DateAdded = dto.DateAdded,
                Protein = dto.Protein,
                Carbs = dto.Carbs,
                Fats = dto.Fats,
                Micronutrients = dto.Micronutrients,
                ServingSize = dto.ServingSize,
                ServingUnit = dto.ServingUnit
            };
        }
    }
}
