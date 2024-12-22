using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OnlineShoppingApp.APIs.Data.Models;
using OnlineShoppingApp.DAL;
using OnlineShoppingApp.DAL.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using System;

namespace OnlineShoppingApp.APIs.Data.Context
{
    public class MyAppDBContext : IdentityDbContext<ApplicationUser>

    {
        public MyAppDBContext(DbContextOptions<MyAppDBContext> options) : base(options) {
          
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
        public DbSet<UOM> UOMs => Set<UOM>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Currency> Currencies => Set<Currency>();
        public DbSet<CartItem> CartItems => Set<CartItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
            new Item { Id = 1, ItemName = "A1", Description = "Desc 1", UomId = 1, QTY = 100, Price = 1500 },
            new Item { Id = 2, ItemName = "A2", Description = "Desc 2", UomId = 2, QTY = 50, Price = 2800 },
            new Item { Id = 3, ItemName = "A3", Description = "Desc 3", UomId = 1, QTY = 200, Price = 200 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, RequestDate = DateTime.Parse("2024-01-26"), Status = "Open", CustomerId = "1000", DiscountPromoCode = "VS02", DiscountValue = 60.00m, TotalPrice = 3940.00m, CurrencyCode = "EGP", ExchangeRate = 1, ForeignPrice = 3940.00m },
                new Order { Id = 2, RequestDate = DateTime.Parse("2024-01-27"), CloseDate = DateTime.Parse("2019-03-27"), Status = "Close", CustomerId = "2000", DiscountPromoCode = "0", DiscountValue = 0.00m, TotalPrice = 2800.00m, CurrencyCode = "USD", ExchangeRate = 30, ForeignPrice = 93.33m },
                new Order { Id = 3, RequestDate = DateTime.Parse("2024-01-29"), Status = "Open", CustomerId = "1000", DiscountPromoCode = "0", DiscountValue = 0.00m, TotalPrice = 4500.00m, CurrencyCode = "USD", ExchangeRate = 30, ForeignPrice = 150.00m }
                );

            modelBuilder.Entity<OrderDetail>().HasData(
            new OrderDetail { Id = 1, OrderId = 1, ItemId = 1, ItemPrice = 1500, Quantity = 2, TotalPrice = 3000 },
            new OrderDetail { Id = 2, OrderId = 1, ItemId = 3, ItemPrice = 200, Quantity = 5, TotalPrice = 1000 },
            new OrderDetail { Id = 3, OrderId = 2, ItemId = 2, ItemPrice = 2800, Quantity = 1, TotalPrice = 2800 },
            new OrderDetail { Id = 4, OrderId = 3, ItemId = 1, ItemPrice = 1500, Quantity = 3, TotalPrice = 4500 }
            );
            modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = "1000", CustomerName = "Mhammad", UserName = "Mhammad.1", Password = "Mh1212" },
            new Customer { Id = "2000", CustomerName = "Ahmad", UserName = "Ahmad.290", Password = "Ah1212" }
            );

            modelBuilder.Entity<UOM>().HasData(
            new UOM { Id = 1, Description = "KG" },
            new UOM { Id = 2, Description = "Liter" }
            );

            modelBuilder.Entity<Currency>().HasData(
            new Currency { Id = 1, CurrencyCode = "USD", ExchangeRate = 1.0m },
             new Currency { Id = 2, CurrencyCode = "EGP", ExchangeRate = 30.0m }
            );
            modelBuilder.Entity<UOM>()
                .HasMany(u => u.Items)
                .WithOne(i => i.Uom)
                .HasForeignKey(i => i.UomId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<Item>()
                .HasMany(i => i.OrderDetails)
                .WithOne(od => od.Item)
                .HasForeignKey(od => od.ItemId);
            base.OnModelCreating(modelBuilder);
        }

    }
}
