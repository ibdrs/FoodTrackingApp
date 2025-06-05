using FoodTracking.Logic.Interfaces;
using FoodTracking.Logic.Dtos;
using System.Threading.Tasks;

namespace FoodTracking.Data
{
    public class LoggedFoodRepository : ILoggedFoodRepository
    {
        private readonly string connectionString;

        public LoggedFoodRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Task<LoggedFoodDto> GetByIdAsync(int id)
        {
            // Implement database query
            throw new System.NotImplementedException();
        }

        public Task AddAsync(LoggedFoodDto loggedFood)
        {
            // Implement database insertion
             throw new System.NotImplementedException();
        }

        public Task UpdateAsync(LoggedFoodDto loggedFood)
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