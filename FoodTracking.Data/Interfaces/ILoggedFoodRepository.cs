using FoodTracking.Logic.Domain;
using System.Threading.Tasks;

namespace FoodTracking.Data.Interfaces
{
    public interface ILoggedFoodRepository
    {
        Task<LoggedFood> GetByIdAsync(int id);
        Task AddAsync(LoggedFood loggedFood);
        Task UpdateAsync(LoggedFood loggedFood);
        Task DeleteAsync(int id);
    }
}