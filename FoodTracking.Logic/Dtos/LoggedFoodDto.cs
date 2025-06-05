namespace FoodTracking.Logic.Dtos
{
    public class LoggedFoodDto
    {
        public int LogId { get; set; }
        public int MealId { get; set; }
        public int FoodId { get; set; }
        public string CustomFoodName { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}