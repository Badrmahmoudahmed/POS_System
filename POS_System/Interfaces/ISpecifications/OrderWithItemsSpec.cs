using POS_System.Infrastructure.SpecificatoinDP;
using POS_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Interfaces.ISpecifications
{
    class OrderWithItemsSpec : Specification<Order>
    {
        public OrderWithItemsSpec()
        {
            Includes.Add(o => o.OrderItems);
        }
        public OrderWithItemsSpec(int id):base(o => o.Id == id)
        {
            Includes.Add(o => o.OrderItems);
        }
    }
}
