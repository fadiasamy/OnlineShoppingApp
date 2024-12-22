namespace OnlineShoppingApp.APIs.Data.Models
{
    public class Item
    {
        public int Id { get; set; } 
        public string ItemName { get; set; }=string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UomId { get; set; }
        public int QTY { get; set; } 
        public decimal Price { get; set; } 

        public UOM? Uom { get; set; } 
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
