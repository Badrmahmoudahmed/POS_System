using Microsoft.EntityFrameworkCore;
using POS_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Infrastructure.Contexts
{
    public class POS_SystemDBContext : DbContext
    {
        public POS_SystemDBContext(DbContextOptions<POS_SystemDBContext> options):base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

    }
}
