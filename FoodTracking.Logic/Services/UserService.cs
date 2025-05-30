using FoodTracking.Data.Dtos;
using FoodTracking.Data.Dtos.API;
using FoodTracking.Logic.Domain;
using FoodTracking.Data.Interfaces;

namespace FoodTracking.Logic.Services
{
    public class UserService
    { 
        IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public async Task<UserDto> RegisterUser(RegisterDto registerDto)
        {
            // Input validation
            if (registerDto == null)
                throw new ArgumentException("User data is required");
            if (string.IsNullOrWhiteSpace(registerDto.Username))
                throw new ArgumentException("Username is required");
            if (string.IsNullOrWhiteSpace(registerDto.Password))
                throw new ArgumentException("Password is required");
            if (registerDto.Username.Length < 3 || registerDto.Username.Length > 50)
                throw new ArgumentException("Username must be between 3 and 50 characters");
            if (registerDto.Password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters long");

            var userDto = new UserDto
            {
                Username = registerDto.Username,
                Password = registerDto.Password,
                DateCreated = DateTime.UtcNow,
                Id = 0
            };

            try
            {
                return await userRepository.AddUser(userDto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error registering user", ex);
            }
        }

        public async Task<UserDto?> LoginUser(LoginDto loginDto)
        {
            // Input validation
            if (loginDto == null)
                throw new ArgumentException("User data is required");
            if (string.IsNullOrWhiteSpace(loginDto.Username))
                throw new ArgumentException("Username is required");
            if (string.IsNullOrWhiteSpace(loginDto.Password))
                throw new ArgumentException("Password is required");

            var userDto = new UserDto
            {
                Username = loginDto.Username,
                Password = loginDto.Password,
                DateCreated = DateTime.UtcNow,
                Id = 0
            };

            try
            {
                var user = await userRepository.ValidateUser(userDto);
                if (user == null)
                    throw new Exception("Invalid username or password");
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error logging in user", ex);
            }
        }
    }
} 