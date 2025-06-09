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
    public interface IMealHistoryRepository
    {
        Task<MealHistoryDto> GetByIdAsync(int id);
        Task AddAsync(MealHistoryDto mealHistory);
        Task UpdateAsync(MealHistoryDto mealHistory);
        Task DeleteAsync(int id);
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    }
}
=======
        Task<IEnumerable<MealHistoryDto>> GetAllAsync();
    }
} 
>>>>>>> Stashed changes
=======
        Task<IEnumerable<MealHistoryDto>> GetAllAsync();
    }
} 
>>>>>>> Stashed changes
