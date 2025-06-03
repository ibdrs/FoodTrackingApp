using FoodTracking.Data.Interfaces;
using FoodTracking.Data.Dtos;
using System.Threading.Tasks;

namespace FoodTracking.Data
{
    public class MealHistoryRepository : IMealHistoryRepository
    {
        private readonly string connectionString;

        public MealHistoryRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Task<MealHistoryDto> GetByIdAsync(int id)
        {
            // Implement database query
            throw new System.NotImplementedException();
        }

        public Task AddAsync(MealHistoryDto mealHistory)
        {
            // Implement database insertion
             throw new System.NotImplementedException();
        }

        public Task UpdateAsync(MealHistoryDto mealHistory)
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