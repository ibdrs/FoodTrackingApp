using FoodTracking.Logic.Dtos;
using FoodTracking.Logic.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FoodTracking.API.Controllers
{
    [ApiController]
    [Route("api/mealtracking")]
    public class MealTrackingController : ControllerBase
    {
        private readonly MealTrackingService _mealTrackingService;

        public MealTrackingController(MealTrackingService mealTrackingService)
        {
            _mealTrackingService = mealTrackingService;
        }

        // POST /api/mealtracking/history
        [HttpPost("history")]
        public async Task<IActionResult> CreateMealHistory([FromBody] MealHistoryDto dto)
        {
            var result = await _mealTrackingService.CreateMealHistoryAsync(dto.UserId, dto.Date);
            return Ok(result);
        }

        // GET /api/mealtracking/history?userId=&date=
        [HttpGet("history")]
        public async Task<IActionResult> GetMealHistory([FromQuery] int userId, [FromQuery] DateTime date)
        {
            var result = await _mealTrackingService.GetMealHistoryByUserAndDateAsync(userId, date);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST /api/mealtracking/meal
        [HttpPost("meal")]
        public async Task<IActionResult> AddMealToHistory([FromBody] MealDto dto)
        {
            var result = await _mealTrackingService.AddMealToHistoryAsync(dto.HistoryId, dto.MealType);
            return Ok(result);
        }

        // GET /api/mealtracking/meal?historyId=
        [HttpGet("meal")]
        public async Task<IActionResult> GetMealsByHistory([FromQuery] int historyId)
        {
            var result = await _mealTrackingService.GetMealsByHistoryAsync(historyId);
            return Ok(result);
        }

        // POST /api/mealtracking/loggedfood
        [HttpPost("loggedfood")]
        public async Task<IActionResult> LogFoodToMeal([FromBody] LoggedFoodDto dto)
        {
            var result = await _mealTrackingService.LogFoodToMealAsync(dto.MealId, dto.FoodId, dto.Quantity, dto.Unit);
            return Ok(result);
        }

        // GET /api/mealtracking/loggedfood?mealId=
        [HttpGet("loggedfood")]
        public async Task<IActionResult> GetLoggedFoodsByMeal([FromQuery] int mealId)
        {
            var result = await _mealTrackingService.GetLoggedFoodsByMealAsync(mealId);
            return Ok(result);
        }
    }
} 