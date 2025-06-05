using FoodTracking.Logic.Dtos;

namespace FoodTracking.Logic.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserDto> AddUser(UserDto userDto);
        public Task<UserDto> ValidateUser(UserDto userDto);
    }
}
