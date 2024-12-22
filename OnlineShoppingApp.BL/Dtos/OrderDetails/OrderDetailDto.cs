using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL.Dtos;

public class OrderDetailDto
{
    public int ItemId { get; set; }
    public decimal ItemPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}
