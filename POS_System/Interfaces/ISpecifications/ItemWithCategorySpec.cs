using POS_System.Infrastructure.SpecificatoinDP;
using POS_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Interfaces.ISpecifications
{
    public class ItemWithCategorySpec : Specification<Item>
    {
        public ItemWithCategorySpec(int catid):base(i => i.CategoryId == catid)
        {
            Includes.Add(i => i.Category);
        }
    }
}
