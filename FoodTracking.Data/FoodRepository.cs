using FoodTracking.Data.Models;
using FoodTracking.Logic.Dtos;
using FoodTracking.Logic.Interfaces;
using System.Data.SqlClient;
using System.Configuration;


namespace FoodTracking.Data
{
    public class FoodRepository : IFoodRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<FoodDto> GetAllFoods()
        {
            List<FoodDto> foods = new List<FoodDto>();
            string sql = "SELECT Id, Name, Description, Calories, DateAdded FROM Foods";

            using (SqlConnection connection = new SqlConnection(_connectionString))
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
                                Calories = reader.IsDBNull(reader.GetOrdinal("Calories")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("Calories")),
                                DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded"))
                            });
                        }
                    }
                }
            }
            return foods;
        }

        public void AddFood(FoodDto food)
        {
            string sql = "INSERT INTO Foods (Name, Description, Calories, DateAdded) VALUES (@Name, @Description, @Calories, @DateAdded)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@Name", food.Name);
                    command.Parameters.AddWithValue("@Description", (object)food.Description);
                    command.Parameters.AddWithValue("@Calories", (object)food.Calories);
                    command.Parameters.AddWithValue("@DateAdded", DateTime.UtcNow);

                    connection.Open();
                    command.ExecuteNonQuery(); // Execute the INSERT command
                }
            }
        }

        public FoodDto? GetFoodById(int id)
        {
            FoodDto? food = null;
            string sql = "SELECT Id, Name, Description, Calories, DateAdded FROM Foods WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Add parameter for Id
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Read a single row
                        {
                            food = new FoodDto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                                Calories = reader.IsDBNull(reader.GetOrdinal("Calories")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("Calories")),
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
            string sql = "UPDATE Foods SET Name = @Name, Description = @Description, Calories = @Calories WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@Name", food.Name);
                    command.Parameters.AddWithValue("@Description", (object)food.Description ?? DBNull.Value); // Handle potential null Description
                    command.Parameters.AddWithValue("@Calories", (object)food.Calories ?? DBNull.Value); // Handle potential null Calories
                    command.Parameters.AddWithValue("@Id", food.Id);

                    connection.Open();
                    command.ExecuteNonQuery(); // Execute the UPDATE command
                }
            }
        }

        public void DeleteFood(int id)
        {
            string sql = "DELETE FROM Foods WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Add parameter for Id
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    command.ExecuteNonQuery(); // Execute the DELETE command
                }
            }
        }
    }
} 