using FoodTracking.Logic.Dtos;

namespace FoodTracking.Logic.Interfaces
{
    public interface IMealRepository
    {
        Task<MealDto> GetByIdAsync(int id);
        Task AddAsync(MealDto meal);
        Task UpdateAsync(MealDto meal);
        Task DeleteAsync(int id);
        Task<IEnumerable<MealDto>> GetAllAsync();
    }
}