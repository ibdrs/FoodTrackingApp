using FoodTracking.Logic.Dtos;
using FoodTracking.Logic.Interfaces;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;


namespace FoodTracking.Data
{
    public class FoodRepository : IFoodRepository
    {
        private string connectionString = String.Empty;
        public FoodRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<FoodDto> GetAllFoods()
        {
            List<FoodDto> foods = new List<FoodDto>();
            string sql = "SELECT Id, Name, Description, Calories, DateAdded FROM Food";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            foods.Add(new FoodDto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                                Calories = reader.IsDBNull(reader.GetOrdinal("Calories")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Calories")),
                                DateAdded = reader.IsDBNull(reader.GetOrdinal("DateAdded")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("DateAdded"))
                            });
                        }
                    }
                }
            }
            return foods;
        }

        public void AddFood(FoodDto food)
        {
            string sql = "INSERT INTO Food (Name, Description, Calories, DateAdded) VALUES (@Name, @Description, @Calories, @DateAdded)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", food.Name);
                    command.Parameters.AddWithValue("@Description", (object)food.Description);
                    command.Parameters.AddWithValue("@Calories", (object)food.Calories);
                    command.Parameters.AddWithValue("@DateAdded", DateTime.UtcNow);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public FoodDto? GetFoodByName(string name)
        {
            FoodDto? food = null;
            string sql = "SELECT Id, Name, Description, Calories, DateAdded FROM Food WHERE Name = @Name";

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
                            food = new FoodDto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Calories = reader.GetDecimal(reader.GetOrdinal("Calories")),
                                DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded"))
                            };
                        }
                    }
                }
            }
            return food;
        }

        public void UpdateFood(FoodDto food)
        {
            string sql = "UPDATE Food SET Name = @Name, Description = @Description, Calories = @Calories WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", food.Name);
                    command.Parameters.AddWithValue("@Description", (object)food.Description ?? DBNull.Value); 
                    command.Parameters.AddWithValue("@Calories", (object)food.Calories ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Id", food.Id);

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
    }
} 