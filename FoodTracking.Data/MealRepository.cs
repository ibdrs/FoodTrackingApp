using FoodTracking.Logic.Interfaces;
using FoodTracking.Logic.Dtos;
using System.Threading.Tasks;

namespace FoodTracking.Data
{
    public class MealRepository : IMealRepository
    {
        private readonly string connectionString;

        public MealRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Task<MealDto> GetByIdAsync(int id)
        {
            // Implement database query
            throw new System.NotImplementedException();
        }

        public Task AddAsync(MealDto meal)
        {
            // Implement database insertion
             throw new System.NotImplementedException();
        }

        public Task UpdateAsync(MealDto meal)
        {
            // Implement database update
             throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            // Implement database deletion
             throw new System.NotImplementedException();
        }
    }
}