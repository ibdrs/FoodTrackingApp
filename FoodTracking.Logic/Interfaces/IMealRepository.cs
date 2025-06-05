using FoodTracking.Logic.Dtos;
using System.Threading.Tasks;

namespace FoodTracking.Logic.Interfaces
{
    public interface IMealRepository
    {
        Task<MealDto> GetByIdAsync(int id);
        Task AddAsync(MealDto meal);
        Task UpdateAsync(MealDto meal);
        Task DeleteAsync(int id);
    }
}