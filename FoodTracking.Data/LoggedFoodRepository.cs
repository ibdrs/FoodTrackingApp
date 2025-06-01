using FoodTracking.Data.Interfaces;
using FoodTracking.Logic.Domain;
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

        public Task<LoggedFood> GetByIdAsync(int id)
        {
            // Implement database query
            throw new System.NotImplementedException();
        }

        public Task AddAsync(LoggedFood loggedFood)
        {
            // Implement database insertion
             throw new System.NotImplementedException();
        }

        public Task UpdateAsync(LoggedFood loggedFood)
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