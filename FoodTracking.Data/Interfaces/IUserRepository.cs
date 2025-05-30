using FoodTracking.Data.Dtos;

namespace FoodTracking.Data.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserDto> AddUser(UserDto userDto);
        public Task<UserDto> ValidateUser(UserDto userDto);
    }
}
