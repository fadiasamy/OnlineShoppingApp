namespace OnlineShoppingApp.APIs.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? CloseDate { get; set; } 
        public string Status { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string DiscountPromoCode { get; set; } = string.Empty;
        public decimal DiscountValue { get; set; }
        public decimal TotalPrice { get; set; }
        public string CurrencyCode { get; set; }=string.Empty;
        public decimal ExchangeRate { get; set; }
        public decimal ForeignPrice { get; set; }

        public Customer? Customer { get; set; } 
        public ICollection<OrderDetail> OrderDetails { get; set; }=new HashSet<OrderDetail>();

       
    }
}
