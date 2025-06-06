using FoodTracking.Logic.Dtos;
using FoodTracking.Logic.Interfaces;
using Microsoft.Data.SqlClient;

namespace FoodTracking.Data
{
    public class MealTypeRepository : IMealTypeRepository
    {
        private string connectionString = String.Empty;
        public MealTypeRepository(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        public List<MealTypeDto> GetAllMealTypes()
        {
            List<MealTypeDto> mealTypes = new List<MealTypeDto>();
            string sql = @"SELECT Id, Name 
                           FROM MealType";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mealTypes.Add(new MealTypeDto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            });
                        }
                    }
                }
            }

            return mealTypes;
        }

        public void AddMealType(MealTypeDto mealType)
        {
            throw new NotImplementedException();
        }

        public void DeleteMealType(int id)
        {
            throw new NotImplementedException();
        }
    }
}
