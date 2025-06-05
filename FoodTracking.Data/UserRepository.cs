using FoodTracking.Logic.Dtos;
using FoodTracking.Logic.Interfaces;
using Microsoft.Data.SqlClient;

namespace FoodTracking.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString;

        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<UserDto> AddUser(UserDto userDto)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // Check if username is taken
                var checkcmd = new SqlCommand("SELECT COUNT(*) FROM [User] WHERE Username = @Username", connection);
                checkcmd.Parameters.AddWithValue("@Username", userDto.Username);
                int count = (int)await checkcmd.ExecuteScalarAsync();
                if (count > 0)
                {
                    throw new Exception("Username is already in use");
                }

                // Add user to database
                var cmd = new SqlCommand(
                    "INSERT INTO [User] (Username, Password, DateCreated) VALUES (@Username, @Password, @DateCreated); SELECT CAST(scope_identity() AS int);",
                    connection);

                cmd.Parameters.AddWithValue("@Username", userDto.Username);
                cmd.Parameters.AddWithValue("@Password", userDto.Password);
                cmd.Parameters.AddWithValue("@DateCreated", userDto.DateCreated);

                var newId = (int)await cmd.ExecuteScalarAsync();
                userDto.Id = newId;

                return userDto;
            }
        }

        public async Task<UserDto?> ValidateUser(UserDto userDto)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var cmd = new SqlCommand("SELECT Id, Username, Password, DateCreated FROM [User] WHERE Username = @Username AND Password = @Password", connection);
                cmd.Parameters.AddWithValue("@Username", userDto.Username);
                cmd.Parameters.AddWithValue("@Password", userDto.Password);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new UserDto
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Username = reader.GetString(reader.GetOrdinal("Username")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated"))
                        };
                    }
                }
            }
            return null;
        }
    }
}