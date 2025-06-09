namespace FoodTracking.Logic.Dtos
{
    public class MealHistoryDto
    {
        public int HistoryId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<MealDto> Meals { get; set; }
    }
}