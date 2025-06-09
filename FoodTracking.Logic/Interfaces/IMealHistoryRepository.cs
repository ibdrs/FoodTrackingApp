using FoodTracking.Logic.Dtos;

namespace FoodTracking.Logic.Interfaces
{
    public interface IMealHistoryRepository
    {
        Task<MealHistoryDto> GetByIdAsync(int id);
        Task AddAsync(MealHistoryDto mealHistory);
        Task UpdateAsync(MealHistoryDto mealHistory);
        Task DeleteAsync(int id);
        Task<IEnumerable<MealHistoryDto>> GetAllAsync();
    }
}
