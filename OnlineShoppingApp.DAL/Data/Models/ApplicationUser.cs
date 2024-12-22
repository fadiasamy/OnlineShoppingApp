using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineShoppingApp.APIs.Data.Models;




namespace OnlineShoppingApp.DAL.Data.Models;

    public class ApplicationUser : IdentityUser
    {
    public string CustomerName { get; set; }= string.Empty; 
    public ICollection<Order> Orders { get; set; }=new HashSet<Order>();

    }

