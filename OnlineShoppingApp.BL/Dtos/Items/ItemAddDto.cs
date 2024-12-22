using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL.Dtos;

    public class ItemAddDto
    {
      [Required]
      [StringLength(100)]
      public string ItemName { get; set; } = string.Empty;

      public string Description { get; set; } = string.Empty;

      [Required]
      public int UomId { get; set; }

      [Required]
      [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0!")]
      public int QTY { get; set; }

      [Required]
      [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0!")]
      public decimal Price { get; set; }
    }

