using FoodTracking.Logic.Dtos;

namespace FoodTracking.Logic.Interfaces
{
    public interface ILoggedFoodRepository
    {
        Task<LoggedFoodDto> GetByIdAsync(int id);
        Task AddAsync(LoggedFoodDto loggedFood);
        Task UpdateAsync(LoggedFoodDto loggedFood);
        Task DeleteAsync(int id);
        Task<IEnumerable<LoggedFoodDto>> GetAllAsync();
    }
}
