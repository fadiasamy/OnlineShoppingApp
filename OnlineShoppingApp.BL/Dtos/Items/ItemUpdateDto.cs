using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL.Dtos;

public class ItemUpdateDto
{
    [Required]
    public int Id { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int UomId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
