using FoodTracking.Logic.Dtos;
using FoodTracking.Logic.Interfaces;
using FoodTracking.Logic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracking.Logic.Services
{
    public class FoodService
    {
        IFoodRepository foodRepository;

        public FoodService(IFoodRepository foodRepository)
        {
            this.foodRepository = foodRepository;
        }

        public List<Food> GetAllFoods()
        {
            List<Food> foods = new List<Food>();
            List<FoodDto> foodsDto;
            try
            {
                foodsDto = foodRepository.GetAllFoods();
            }
            catch (Exception ex) {
                throw new Exception("Error getting the foods", ex);
            }

            foreach (FoodDto foodDto in foodsDto)
            {
                foods.Add(new Food(foodDto));
            }
            return foods;
        }
        public Food GetFoodByName(string name)
        {
            FoodDto foodDto;
            try
            {
                foodDto = foodRepository.GetFoodByName(name);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting food by name {name}", ex);
            }
            

            if (foodDto == null)
            {
                return null;
            }

            return new Food(foodDto);
        }
        public void AddFood(FoodDto food)
        {
            try
            {
                foodRepository.AddFood(food);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding food", ex);
            }
        }

        public void UpdateFood(FoodDto food)
        {
            try
            {
                foodRepository.UpdateFood(food);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating food with id {food.Id}", ex);
            }
        }
        public void DeleteFood(int id)
        {
            try
            {
                foodRepository.DeleteFood(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting food with id {id}", ex);
            }
        }
    }
}
