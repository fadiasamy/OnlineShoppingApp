namespace OnlineShoppingApp.APIs.Data.Models
{
    public class UOM
    {
        public int Id { get; set; }
        public string UOMName { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;

        public ICollection<Item> Items { get; set; } = new HashSet<Item>();
    }
}
