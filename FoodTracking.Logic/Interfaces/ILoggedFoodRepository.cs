using FoodTracking.Logic.Dtos;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
using System.Threading.Tasks;
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes

namespace FoodTracking.Logic.Interfaces
{
    public interface ILoggedFoodRepository
    {
        Task<LoggedFoodDto> GetByIdAsync(int id);
        Task AddAsync(LoggedFoodDto loggedFood);
        Task UpdateAsync(LoggedFoodDto loggedFood);
        Task DeleteAsync(int id);
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    }
}
=======
        Task<IEnumerable<LoggedFoodDto>> GetAllAsync();
    }
} 
>>>>>>> Stashed changes
=======
        Task<IEnumerable<LoggedFoodDto>> GetAllAsync();
    }
} 
>>>>>>> Stashed changes
