using Microsoft.Data.SqlClient;
using FoodTracking.Data.Dtos;
using FoodTracking.Data.Interfaces;

namespace FoodTracking.Data
{
    public class FoodRepository : IFoodRepository
    {
        private string connectionString = String.Empty;
        public FoodRepository(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<FoodDto> GetAllFoods()
        {
            List<FoodDto> foods = new List<FoodDto>();
            string sql = @"SELECT Id, Name, Description, Calories, DateAdded, 
                                 Protein, Carbs, Fats, Micronutrients, 
                                 ServingSize, ServingUnit 
                          FROM Food";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            foods.Add(MapReaderToFoodDto(reader));
                        }
                    }
                }
            }
            return foods;
        }

        public void AddFood(FoodDto food)
        {
            string sql = @"
                INSERT INTO Food 
                (Name, Description, Calories, DateAdded, Protein, Carbs, Fats, Micronutrients, ServingSize, ServingUnit) 
                VALUES 
                (@Name, @Description, @Calories, @DateAdded, @Protein, @Carbs, @Fats, @Micronutrients, @ServingSize, @ServingUnit)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", food.Name);
                    command.Parameters.AddWithValue("@Description", (object?)food.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Calories", food.Calories);
                    command.Parameters.AddWithValue("@DateAdded", food.DateAdded);
                    command.Parameters.AddWithValue("@Protein", food.Protein);
                    command.Parameters.AddWithValue("@Carbs", food.Carbs);
                    command.Parameters.AddWithValue("@Fats", food.Fats);
                    command.Parameters.AddWithValue("@Micronutrients", (object?)food.Micronutrients ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ServingSize", food.ServingSize);
                    command.Parameters.AddWithValue("@ServingUnit", food.ServingUnit);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public FoodDto? GetFoodByName(string name)
        {
            FoodDto? food = null;
            string sql = @"SELECT Id, Name, Description, Calories, DateAdded, 
                                 Protein, Carbs, Fats, Micronutrients, 
                                 ServingSize, ServingUnit 
                          FROM Food 
                          WHERE Name = @Name";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Reads one row
                        {
                            food = MapReaderToFoodDto(reader);
                        }
                    }
                }
            }
            return food;
        }

        public void UpdateFood(FoodDto food)
        {
            string sql = @"UPDATE Food SET 
                    Name = @Name, 
                    Description = @Description, 
                    Calories = @Calories, 
                    Protein = @Protein,
                    Carbs = @Carbs,
                    Fats = @Fats,
                    Micronutrients = @Micronutrients,
                    ServingSize = @ServingSize,
                    ServingUnit = @ServingUnit
                    WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", food.Id);
                    command.Parameters.AddWithValue("@Name", food.Name);
                    command.Parameters.AddWithValue("@Description", (object?)food.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Calories", food.Calories);
                    command.Parameters.AddWithValue("@Protein", food.Protein);
                    command.Parameters.AddWithValue("@Carbs", food.Carbs);
                    command.Parameters.AddWithValue("@Fats", food.Fats);
                    command.Parameters.AddWithValue("@Micronutrients", (object?)food.Micronutrients ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ServingSize", food.ServingSize);
                    command.Parameters.AddWithValue("@ServingUnit", food.ServingUnit);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteFood(int id)
        {
            string sql = "DELETE FROM Food WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private static FoodDto MapReaderToFoodDto(SqlDataReader reader)
        {
            return new FoodDto
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Description")),
                Calories = reader.GetDecimal(reader.GetOrdinal("Calories")),
                DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded")),
                Protein = reader.GetDecimal(reader.GetOrdinal("Protein")),
                Carbs = reader.GetDecimal(reader.GetOrdinal("Carbs")),
                Fats = reader.GetDecimal(reader.GetOrdinal("Fats")),
                Micronutrients = reader.IsDBNull(reader.GetOrdinal("Micronutrients"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Micronutrients")),
                ServingSize = reader.GetDecimal(reader.GetOrdinal("ServingSize")),
                ServingUnit = reader.GetString(reader.GetOrdinal("ServingUnit"))
            };
        }
    }
}

