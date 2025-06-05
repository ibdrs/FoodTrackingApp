using System.Collections.Generic;

namespace FoodTracking.Logic.Dtos
{
    public class MealDto
    {
        public int Id { get; set; }
        public int HistoryId { get; set; }
        public string MealType { get; set; }
        public ICollection<LoggedFoodDto> LoggedFoods { get; set; }
    }
}