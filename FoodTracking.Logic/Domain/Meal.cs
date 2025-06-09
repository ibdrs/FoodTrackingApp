

namespace FoodTracking.Logic.Domain
{
    public class Meal
    {
        public int Id { get; set; }
        public int HistoryId { get; set; }
        public string MealType { get; set; }

        // Navigation properties
        public MealHistory MealHistory { get; set; }
        public ICollection<LoggedFood> LoggedFoods { get; set; }
    }
}