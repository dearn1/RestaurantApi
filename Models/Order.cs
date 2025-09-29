namespace RestaurantApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Dish> Dishes { get; set; }
    }
}
