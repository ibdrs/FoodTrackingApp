using FoodTracking.Logic.Dtos;
using System.Threading.Tasks;

namespace FoodTracking.Logic.Interfaces
{
    public interface ILoggedFoodRepository
    {
        Task<LoggedFoodDto> GetByIdAsync(int id);
        Task AddAsync(LoggedFoodDto loggedFood);
        Task UpdateAsync(LoggedFoodDto loggedFood);
        Task DeleteAsync(int id);
    }
}