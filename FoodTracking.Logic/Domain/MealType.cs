using FoodTracking.Logic.Dtos;

namespace FoodTracking.Logic.Domain
{
    public class MealType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public MealType() { }

        public MealType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public MealType(MealTypeDto mealTypeDto)
        {
            Id = mealTypeDto.Id;
            Name = mealTypeDto.Name;
        }

        public MealTypeDto GetMealTypeDto(MealType mealType)
        {
            return new MealTypeDto
            {
                Id = mealType.Id,
                Name = mealType.Name,
            };
        }

        public MealType FromDto(MealTypeDto dto)
        {
            return new MealType
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }
    }
}
