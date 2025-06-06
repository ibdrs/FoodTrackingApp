using FoodTracking.Logic.Dtos;
using System.Threading.Tasks;

namespace FoodTracking.Logic.Interfaces
{
    public interface IMealHistoryRepository
    {
        Task<MealHistoryDto> GetByIdAsync(int id);
        Task AddAsync(MealHistoryDto mealHistory);
        Task UpdateAsync(MealHistoryDto mealHistory);
        Task DeleteAsync(int id);
    }
}