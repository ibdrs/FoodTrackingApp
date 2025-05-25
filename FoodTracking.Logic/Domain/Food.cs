using FoodTracking.Logic.Dtos;

namespace FoodTracking.Logic.Domain
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Calories { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

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
        }

        public FoodDto GetFoodDto(Food food)
        {
            return new FoodDto() { Id = Id, Name = Name, Description = Description, Calories = Calories, DateAdded = DateAdded };
        }

    }
}
