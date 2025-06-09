using FoodTracking.Logic.Interfaces;
using FoodTracking.Logic.Dtos;
using Microsoft.Data.SqlClient;

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

        public async Task<IEnumerable<MealDto>> GetAllAsync()
        {
            var result = new List<MealDto>();
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT Id, HistoryId, MealType FROM Meal", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new MealDto
                        {
                            Id = reader.GetInt32(0),
                            HistoryId = reader.GetInt32(1),
                            MealType = reader.GetString(2),
                            LoggedFoods = new List<LoggedFoodDto>() // Not loaded here
                        });
                    }
                }
            }
            return result;
        }
    }
}