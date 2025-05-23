using FoodTracking.Logic.Dtos;
using FoodTracking.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodTracking.Data
{
    public class FoodRepository : IFoodRepository
    {
        private readonly FoodDbContext _context;

        public FoodRepository(FoodDbContext context)
        {
            _context = context;
        }

        public List<FoodDto> GetAllFoods()
        {
            return _context.Foods
                .Select(f => new FoodDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    Description = f.Description,
                    Calories = f.Calories,
                    DateAdded = f.DateAdded
                })
                .ToList();
        }

        public void AddFood(FoodDto food)
        {
            var foodEntity = new Food
            {
                Name = food.Name,
                Description = food.Description,
                Calories = food.Calories,
                DateAdded = DateTime.UtcNow
            };

            _context.Foods.Add(foodEntity);
            _context.SaveChanges();
        }

        public FoodDto? GetFoodById(int id)
        {
            var food = _context.Foods.Find(id);
            if (food == null) return null;

            return new FoodDto
            {
                Id = food.Id,
                Name = food.Name,
                Description = food.Description,
                Calories = food.Calories,
                DateAdded = food.DateAdded
            };
        }

        public void UpdateFood(FoodDto food)
        {
            var existingFood = _context.Foods.Find(food.Id);
            if (existingFood == null) return;

            existingFood.Name = food.Name;
            existingFood.Description = food.Description;
            existingFood.Calories = food.Calories;

            _context.SaveChanges();
        }

        public void DeleteFood(int id)
        {
            var food = _context.Foods.Find(id);
            if (food == null) return;

            _context.Foods.Remove(food);
            _context.SaveChanges();
        }
    }
} 