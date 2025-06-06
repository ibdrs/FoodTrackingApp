using FoodTracking.Logic.Dtos;
using FoodTracking.Logic.Interfaces;
using FoodTracking.Logic.Domain;

namespace FoodTracking.Logic.Services
{
    public class MealTypeService
    {
        IMealTypeRepository mealTypeRepository;

        public MealTypeService(IMealTypeRepository mealTypeRepository)
        {
            this.mealTypeRepository = mealTypeRepository;
        }

        public List<MealType> GetAllTypes()
        {
            List<MealType> mealTypes = new List<MealType>();
            List<MealTypeDto> mealTypeDtos;
            try
            {
                mealTypeDtos = mealTypeRepository.GetAllMealTypes();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting the meal types", ex);
            }

            foreach (MealTypeDto MealTypeDto in mealTypeDtos)
            {
                mealTypes.Add(new MealType(MealTypeDto));
            }

            return mealTypes;
        }
    }
}
