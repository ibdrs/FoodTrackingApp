using FoodTracking.Logic.Interfaces;
using FoodTracking.Logic.Dtos;
using Microsoft.Data.SqlClient;

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

        public async Task<IEnumerable<LoggedFoodDto>> GetAllAsync()
        {
            var result = new List<LoggedFoodDto>();
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT LogId, MealId, FoodId, Quantity, Unit FROM LoggedFood", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new LoggedFoodDto
                        {
                            LogId = reader.GetInt32(0),
                            MealId = reader.GetInt32(1),
                            FoodId = reader.GetInt32(2),
                            Quantity = reader.GetDecimal(3),
                            Unit = reader.GetString(4)
                        });
                    }
                }
            }
            return result;
        }
    }
}