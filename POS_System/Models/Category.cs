namespace POS_System.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public ICollection<Item> Items { get; set; } = new HashSet<Item>();
    }
}
