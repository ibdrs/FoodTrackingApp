namespace FoodTracking.Logic.Domain
{
    public class LoggedFood
    {
        public int LogId { get; set; }
        public int MealId { get; set; }
        public int FoodId { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }

        // Navigation properties
        public Meal Meal { get; set; }
        public Food Food { get; set; }
    }
}