using FoodTracking.Logic.Dtos;
using FoodTracking.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodTracking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;

        public FoodController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FoodDto>> GetAll()
        {
            return Ok(_foodRepository.GetAllFoods());
        }

        [HttpGet("{id}")]
        public ActionResult<FoodDto> GetById(int id)
        {
            var food = _foodRepository.GetFoodById(id);
            if (food == null)
                return NotFound();

            return Ok(food);
        }

        [HttpPost]
        public IActionResult Create(FoodDto food)
        {
            _foodRepository.AddFood(food);
            return CreatedAtAction(nameof(GetById), new { id = food.Id }, food);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, FoodDto food)
        {
            if (id != food.Id)
                return BadRequest();

            _foodRepository.UpdateFood(food);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _foodRepository.DeleteFood(id);
            return NoContent();
        }
    }
} 