using FoodTracking.Logic.Interfaces;
using FoodTracking.Logic.Dtos;
using Microsoft.Data.SqlClient;

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

        public async Task<IEnumerable<MealHistoryDto>> GetAllAsync()
        {
            var result = new List<MealHistoryDto>();
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT HistoryId, UserId, Date FROM MealHistory", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new MealHistoryDto
                        {
                            HistoryId = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            Date = reader.GetDateTime(2),
                            Meals = new List<MealDto>() // Not loaded here
                        });
                    }
                }
            }
            return result;
        }
    }
}