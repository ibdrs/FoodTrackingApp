using FoodTracking.Data.Interfaces;
using FoodTracking.Logic.Domain;
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

        public Task<Meal> GetByIdAsync(int id)
        {
            // Implement database query
            throw new System.NotImplementedException();
        }

        public Task AddAsync(Meal meal)
        {
            // Implement database insertion
             throw new System.NotImplementedException();
        }

        public Task UpdateAsync(Meal meal)
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