namespace FoodTracking.Logic.Domain
{
    public class MealHistory
    {
        public int HistoryId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }

        // Navigation properties
        public User User { get; set; }
        public ICollection<Meal> Meals { get; set; }
    }
}