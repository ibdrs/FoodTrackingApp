using FoodTracking.Logic.Dtos;
using FoodTracking.Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealTypeController : ControllerBase
    {
        private MealTypeService mealTypeService;
        private readonly ILogger<MealTypeController> logger;

        public MealTypeController(MealTypeService mealTypeService, ILogger<MealTypeController> logger)
        {
            this.logger = logger;
            this.mealTypeService = mealTypeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MealTypeDto>> GetAll()
        {
            var mealTypes = mealTypeService.GetAllTypes();
            var mealTypeDtos = mealTypes.Select(m => m.GetMealTypeDto(m)).ToList();
            return Ok(mealTypeDtos);
        }
    }
}
