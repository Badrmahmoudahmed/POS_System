namespace POS_System.Models
{
    public class OrderItem : BaseEntity
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }

    }
}
