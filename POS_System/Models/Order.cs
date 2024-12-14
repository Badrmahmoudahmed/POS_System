namespace POS_System.Models
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public int ItemsCount { get; set; }
        public decimal OrderPrice { get; set; }
        public string PaymentMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}
