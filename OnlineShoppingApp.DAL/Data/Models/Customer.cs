namespace OnlineShoppingApp.APIs.Data.Models
{
    public class Customer
    {
        public string Id { get; set; } =string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
