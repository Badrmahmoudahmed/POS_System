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
            if (dBContext.Categories.Any())
            { return; }
            // Seed Categories
            var category1 = new Category { Id = 1, Name = "Appetizers", Icon = "nachos.png" };
            var category2 = new Category { Id = 2, Name = "Desserts", Icon = "dessert.png" };
            var category3 = new Category { Id = 3, Name = "Beverages", Icon = "fastfood.png" };
            var category4 = new Category { Id = 4, Name = "Salads", Icon = "salad.png" };
            var category5 = new Category { Id = 5, Name = "Soups", Icon = "hotsoup.png" };

            dBContext.Set<Category>().AddRange(category1, category2, category3, category4, category5);

            // Seed Items
            var items = new List<Item>
                            {
                                // Appetizers
                                new Item { Name = "Spring Rolls", Icon = "springrolls.png", Description = "Crispy vegetable spring rolls.", Price = 4.99m, CategoryId = 1 },
                                new Item { Name = "Stuffed Mushrooms", Icon = "mushroom.png", Description = "Mushrooms filled with cheese and herbs.", Price = 6.49m, CategoryId = 1 },
                                new Item { Name = "Garlic Bread", Icon = "garlicbread.png", Description = "Freshly baked garlic bread with herbs.", Price = 3.99m, CategoryId = 1 },
                                new Item { Name = "Mozzarella Sticks", Icon = "mozzarella.png", Description = "Golden-fried mozzarella cheese sticks.", Price = 5.99m, CategoryId = 1 },
                                new Item { Name = "Chicken Wings", Icon = "chickenwings.png", Description = "Spicy buffalo chicken wings.", Price = 7.99m, CategoryId = 1 },
                                new Item { Name = "Bruschetta", Icon = "bruschetta.png", Description = "Grilled bread with tomato and basil.", Price = 4.49m, CategoryId = 1 },

                                // Desserts
                                new Item { Name = "Chocolate Cake", Icon = "blackforest.png", Description = "Rich chocolate cake with fudge frosting.", Price = 6.99m, CategoryId = 2 },
                                new Item { Name = "Cheesecake", Icon = "cheesecake.png", Description = "Classic creamy cheesecake.", Price = 5.99m, CategoryId = 2 },
                                new Item { Name = "Ice Cream Sundae", Icon = "sundae.png", Description = "Vanilla ice cream with chocolate syrup and toppings.", Price = 4.99m, CategoryId = 2 },
                                new Item { Name = "Apple Pie", Icon = "applepie.png", Description = "Warm apple pie with cinnamon.", Price = 5.49m, CategoryId = 2 },
                                new Item { Name = "Brownies", Icon = "brownie.png", Description = "Fudgy chocolate brownies.", Price = 4.99m, CategoryId = 2 },
                                new Item { Name = "Tiramisu", Icon = "tiramisu.png", Description = "Italian coffee-flavored dessert.", Price = 6.49m, CategoryId = 2 },

                                // Beverages
                                new Item { Name = "Coca-Cola", Icon = "cola.png", Description = "Chilled can of Coca-Cola.", Price = 1.99m, CategoryId = 3 },
                                new Item { Name = "Orange Juice", Icon = "orangejuice.png", Description = "Freshly squeezed orange juice.", Price = 3.49m, CategoryId = 3 },
                                new Item { Name = "Coffee", Icon = "coffeecup.png", Description = "Hot brewed coffee.", Price = 2.99m, CategoryId = 3 },
                                new Item { Name = "Tea", Icon = "tea.png", Description = "A cup of hot tea.", Price = 2.49m, CategoryId = 3 },
                                new Item { Name = "Smoothie", Icon = "smoothie.png", Description = "Mixed fruit smoothie.", Price = 4.49m, CategoryId = 3 },
                                new Item { Name = "Mineral Water", Icon = "minerals.png", Description = "Bottled still water.", Price = 1.49m, CategoryId = 3 },

                                // Salads
                                new Item { Name = "Caesar Salad", Icon = "fruitbowl.png", Description = "Crisp romaine lettuce with Caesar dressing.", Price = 7.99m, CategoryId = 4 },
                                new Item { Name = "Greek Salad", Icon = "salad2.png", Description = "Fresh vegetables with feta cheese and olives.", Price = 6.99m, CategoryId = 4 },
                                new Item { Name = "Garden Salad", Icon = "vegetable.png", Description = "A mix of fresh garden vegetables.", Price = 5.99m, CategoryId = 4 },
                                new Item { Name = "Pasta Salad", Icon = "spaguetti.png", Description = "Pasta with fresh vegetables and dressing.", Price = 6.49m, CategoryId = 4 },
                                new Item { Name = "Quinoa Salad", Icon = "food.png", Description = "Healthy quinoa salad with vegetables.", Price = 7.49m, CategoryId = 4 },
                                new Item { Name = "Caprese Salad", Icon = "caprese.png", Description = "Tomatoes, mozzarella, and basil.", Price = 6.49m, CategoryId = 4 },

                                // Soups
                                new Item { Name = "Tomato Soup", Icon = "meatball.png", Description = "Creamy tomato soup.", Price = 4.99m, CategoryId = 5 },
                                new Item { Name = "Chicken Noodle Soup", Icon = "ramen.png", Description = "Classic chicken noodle soup.", Price = 5.49m, CategoryId = 5 },
                                new Item { Name = "Minestrone", Icon = "minestrone.png", Description = "Hearty Italian vegetable soup.", Price = 6.49m, CategoryId = 5 },
                                new Item { Name = "Pumpkin Soup", Icon = "pumpkin.png", Description = "Smooth and creamy pumpkin soup.", Price = 5.99m, CategoryId = 5 },
                                new Item { Name = "Lentil Soup", Icon = "hotsoup.png", Description = "Healthy and filling lentil soup.", Price = 5.49m, CategoryId = 5 },
                                new Item { Name = "Clam Chowder", Icon = "clamchowder.png", Description = "Rich and creamy clam chowder.", Price = 6.99m, CategoryId = 5 }
                            };


            dBContext.Set<Item>().AddRange(items);
            dBContext.SaveChanges();
        }
    }

}
