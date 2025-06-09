using FoodTracking.Logic.Dtos;

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

        public MealHistory() { }
        public MealHistory(int historyId, int userId, DateTime date)
        {
            HistoryId = historyId;
            UserId = userId;
            Date = date;
        }

        public MealHistory(MealHistoryDto mealHistoryDto)
        {
            HistoryId = mealHistoryDto.HistoryId;
            UserId = mealHistoryDto.UserId;
            Date = mealHistoryDto.Date;
        }
    }
}