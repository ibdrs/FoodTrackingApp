using FoodTracking.Logic.Dtos;

namespace FoodTracking.Logic.Interfaces
{
    public interface IFoodRepository
    {
        public List<FoodDto> GetAllFoods();
        public void AddFood(FoodDto food);
        public FoodDto? GetFoodById(int id);
        public void UpdateFood(FoodDto food);
        public void DeleteFood(int id);
    }
} 