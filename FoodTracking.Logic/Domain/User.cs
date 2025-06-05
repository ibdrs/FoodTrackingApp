using FoodTracking.Logic.Dtos;

namespace FoodTracking.Logic.Domain
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public User(int id, string username, string password, DateTime dateCreated)
        {
            Id = id;
            Username = username;
            Password = password;
            DateCreated = dateCreated;
        }

        public User(UserDto userDto){
            Id = userDto.Id;
            Username = userDto.Username;
            Password = userDto.Password;
            DateCreated = userDto.DateCreated;
        }

        public UserDto GetUserDto(User user){
            return new UserDto(){
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                DateCreated = user.DateCreated
            };
        }
    }
}