using OnlineShoppingApp.APIs.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL.Dtos;

    public class ItemReadDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public String UOM { get; set; } = string.Empty;
        public int QTY { get; set; }
        public decimal Price { get; set; }

    }

