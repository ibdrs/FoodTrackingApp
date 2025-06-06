using FoodTracking.Logic.Dtos;

namespace FoodTracking.Logic.Interfaces
{
    public interface IMealTypeRepository
    {
        public List<MealTypeDto> GetAllMealTypes();
        public void AddMealType(MealTypeDto mealType);
        public void DeleteMealType(int id);
    }
}
