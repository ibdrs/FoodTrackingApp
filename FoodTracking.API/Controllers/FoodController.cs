using FoodTracking.Data.Dtos;
using FoodTracking.Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodTracking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        private FoodService foodService;
        private readonly ILogger<FoodController> logger;

        public FoodController(FoodService foodService, ILogger<FoodController> logger)
        {
            this.logger = logger;
            this.foodService = foodService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FoodDto>> GetAll()
        {
            var foods = foodService.GetAllFoods();
            var foodDtos = foods.Select(f => f.GetFoodDto(f)).ToList();
            return Ok(foodDtos);
        }

        [HttpGet("{name}")]
        public ActionResult<FoodDto> GetByName(string name)
        {
            try
            {
                var food = foodService.GetFoodByName(name);
                if (food == null)
                {
                    return NotFound();
                }
                return Ok(food.GetFoodDto(food));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting food by name");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult Create(FoodDto food)
        {
            try
            {
                foodService.AddFood(food);
                return StatusCode(201, food);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating food");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, FoodDto food)
        {
            if (id != food.Id)
            {
                return BadRequest("Food ID in URL does not match Food ID in body");
            }

            try
            {
                foodService.UpdateFood(food);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating food");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                foodService.DeleteFood(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting food");
                return StatusCode(500, "Internal server error");
            }
        }
    }
} 