using Microsoft.EntityFrameworkCore;
using POS_System.Infrastructure.Contexts;
using POS_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Infrastructure
{
    public static class SeedData
    {
        public static void Seed(POS_SystemDBContext dBContext)
        {
            if(dBContext.Categories.Any())
                { return; }
            // Seed Categories
            var category1 = new Category { Id = 1, Name = "Appetizers", Icon = "appetizers_icon.png" };
            var category2 = new Category { Id = 2, Name = "Desserts", Icon = "desserts_icon.png" };
            var category3 = new Category { Id = 3, Name = "Beverages", Icon = "beverages_icon.png" };
            var category4 = new Category { Id = 4, Name = "Salads", Icon = "salads_icon.png" };
            var category5 = new Category { Id = 5, Name = "Soups", Icon = "soups_icon.png" };

            dBContext.Set<Category>().AddRange(category1, category2, category3, category4, category5);

            // Seed Items
            var items = new List<Item>();
            int itemId = 1;

            // Generate items for each category
            foreach (var category in new[] { category1, category2, category3, category4, category5 })
            {
                for (int i = 1; i <= 10; i++)
                {
                    items.Add(new Item
                    {
                        Id = itemId++,
                        Name = $"{category.Name} Item {i}",
                        Icon = $"{category.Name.ToLower()}_item_{i}_icon.png",
                        Description = $"Description for {category.Name} Item {i}",
                        Price = 5.99m + i,
                        Category = category
                    });
                }
            }

            dBContext.Set<Item>().AddRange(items);

            // Seed Orders
            var order1 = new Order
            {
                Id = 1,
                OrderDate = DateTime.UtcNow,
                ItemsCount = 2,
                OrderPrice = 18.98m,
                PaymentMethod = "Credit Card"
            };

            dBContext.Set<Order>().Add(order1);

            // Seed OrderItems
            var orderItems = new List<OrderItem>();

            // Add order items from the first two categories for the order
            orderItems.Add(new OrderItem
            {
                Id = 1,
                Name = items[0].Name,
                Icon = items[0].Icon,
                Price = items[0].Price,
                Quantity = 2,
                Item = items[0],

            });

            orderItems.Add(new OrderItem
            {
                Id = 2,
                Name = items[10].Name,
                Icon = items[10].Icon,
                Price = items[10].Price,
                Quantity = 1,
                Item = items[10],

            });

            dBContext.Set<OrderItem>().AddRange(orderItems);
            dBContext.SaveChanges();
        }
    }

}
