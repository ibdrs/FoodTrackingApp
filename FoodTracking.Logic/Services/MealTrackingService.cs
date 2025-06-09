using FoodTracking.Logic.Dtos;
using FoodTracking.Logic.Interfaces;

namespace FoodTracking.Logic.Services
{
    public class MealTrackingService
    {
        private readonly IMealHistoryRepository _mealHistoryRepository;
        private readonly IMealRepository _mealRepository;
        private readonly ILoggedFoodRepository _loggedFoodRepository;

        public MealTrackingService(
            IMealHistoryRepository mealHistoryRepository,
            IMealRepository mealRepository,
            ILoggedFoodRepository loggedFoodRepository)
        {
            _mealHistoryRepository = mealHistoryRepository;
            _mealRepository = mealRepository;
            _loggedFoodRepository = loggedFoodRepository;
        }

        // Create a new meal history for a user and date
        public async Task<MealHistoryDto> CreateMealHistoryAsync(int userId, DateTime date)
        {
            var mealHistoryDto = new MealHistoryDto
            {
                UserId = userId,
                Date = date,
                Meals = new List<MealDto>()
            };
            await _mealHistoryRepository.AddAsync(mealHistoryDto);
            return new MealHistoryDto
            {
                HistoryId = mealHistoryDto.HistoryId,
                UserId = mealHistoryDto.UserId,
                Date = mealHistoryDto.Date,
                Meals = new List<MealDto>()
            };
        }

        // Get meal history for a user and date
        public async Task<MealHistoryDto> GetMealHistoryByUserAndDateAsync(int userId, DateTime date)
        {
            // This assumes you have a method to get by user and date, otherwise filter after fetching
            var allHistories = await _mealHistoryRepository.GetAllAsync();
            var history = allHistories.FirstOrDefault(h => h.UserId == userId && h.Date.Date == date.Date);
            if (history == null) return null;
            return new MealHistoryDto
            {
                HistoryId = history.HistoryId,
                UserId = history.UserId,
                Date = history.Date,
                Meals = history.Meals?.Select(m => new MealDto
                {
                    Id = m.Id,
                    HistoryId = m.HistoryId,
                    MealType = m.MealType,
                    LoggedFoods = m.LoggedFoods?.Select(lf => new LoggedFoodDto
                    {
                        LogId = lf.LogId,
                        MealId = lf.MealId,
                        FoodId = lf.FoodId,
                        Quantity = lf.Quantity,
                        Unit = lf.Unit
                    }).ToList() ?? new List<LoggedFoodDto>()
                }).ToList() ?? new List<MealDto>()
            };
        }

        // Add a meal to a meal history
        public async Task<MealDto> AddMealToHistoryAsync(int historyId, string mealType)
        {
            var mealDto = new MealDto
            {
                HistoryId = historyId,
                MealType = mealType,
                LoggedFoods = new List<LoggedFoodDto>()
            };
            await _mealRepository.AddAsync(mealDto);
            return new MealDto
            {
                Id = mealDto.Id,
                HistoryId = mealDto.HistoryId,
                MealType = mealDto.MealType,
                LoggedFoods = new List<LoggedFoodDto>()
            };
        }

        // Get meals under a meal history
        public async Task<List<MealDto>> GetMealsByHistoryAsync(int historyId)
        {
            // This assumes you have a method to get all meals, otherwise filter after fetching
            var allMeals = await _mealRepository.GetAllAsync();
            var meals = allMeals.Where(m => m.HistoryId == historyId).ToList();
            return meals.Select(m => new MealDto
            {
                Id = m.Id,
                HistoryId = m.HistoryId,
                MealType = m.MealType,
                LoggedFoods = m.LoggedFoods?.Select(lf => new LoggedFoodDto
                {
                    LogId = lf.LogId,
                    MealId = lf.MealId,
                    FoodId = lf.FoodId,
                    Quantity = lf.Quantity,
                    Unit = lf.Unit
                }).ToList() ?? new List<LoggedFoodDto>()
            }).ToList() ?? new List<MealDto>();
        }

        // Log food to a meal
        public async Task<LoggedFoodDto> LogFoodToMealAsync(int mealId, int foodId, decimal quantity, string unit)
        {
            var loggedFoodDto = new LoggedFoodDto
            {
                MealId = mealId,
                FoodId = foodId,
                Quantity = quantity,
                Unit = unit
            };
            await _loggedFoodRepository.AddAsync(loggedFoodDto);
            return new LoggedFoodDto
            {
                LogId = loggedFoodDto.LogId,
                MealId = loggedFoodDto.MealId,
                FoodId = loggedFoodDto.FoodId,
                Quantity = loggedFoodDto.Quantity,
                Unit = loggedFoodDto.Unit
            };
        }

        // Get logged foods under a meal
        public async Task<List<LoggedFoodDto>> GetLoggedFoodsByMealAsync(int mealId)
        {
            // This assumes you have a method to get all logged foods, otherwise filter after fetching
            var allLoggedFoods = await _loggedFoodRepository.GetAllAsync();
            var loggedFoods = allLoggedFoods.Where(lf => lf.MealId == mealId).ToList();
            return loggedFoods.Select(lf => new LoggedFoodDto
            {
                LogId = lf.LogId,
                MealId = lf.MealId,
                FoodId = lf.FoodId,
                Quantity = lf.Quantity,
                Unit = lf.Unit
            }).ToList() ?? new List<LoggedFoodDto>();
        }
    }
} 