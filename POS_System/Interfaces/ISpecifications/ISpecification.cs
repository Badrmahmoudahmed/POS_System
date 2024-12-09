using POS_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Interfaces.ISpecifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Cretria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }
    }
}
