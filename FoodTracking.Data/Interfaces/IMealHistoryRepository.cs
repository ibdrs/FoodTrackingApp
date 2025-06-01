using FoodTracking.Logic.Domain;
using System.Threading.Tasks;

namespace FoodTracking.Data.Interfaces
{
    public interface IMealHistoryRepository
    {
        Task<MealHistory> GetByIdAsync(int id);
        Task AddAsync(MealHistory mealHistory);
        Task UpdateAsync(MealHistory mealHistory);
        Task DeleteAsync(int id);
    }
}