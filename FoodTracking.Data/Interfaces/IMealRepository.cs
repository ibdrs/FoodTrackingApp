using FoodTracking.Logic.Domain;
using System.Threading.Tasks;

namespace FoodTracking.Data.Interfaces
{
    public interface IMealRepository
    {
        Task<Meal> GetByIdAsync(int id);
        Task AddAsync(Meal meal);
        Task UpdateAsync(Meal meal);
        Task DeleteAsync(int id);
    }
}