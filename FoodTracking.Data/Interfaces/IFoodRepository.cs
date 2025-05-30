using FoodTracking.Data.Dtos;

namespace FoodTracking.Data.Interfaces
{
    public interface IFoodRepository
    {
        public List<FoodDto> GetAllFoods();
        public void AddFood(FoodDto food);
        public FoodDto? GetFoodByName(string name);
        public void UpdateFood(FoodDto food);
        public void DeleteFood(int id);
    }
} 