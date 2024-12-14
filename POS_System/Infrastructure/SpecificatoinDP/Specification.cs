using POS_System.Interfaces.ISpecifications;
using POS_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Infrastructure.SpecificatoinDP
{
    public class Specification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Cretria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new();

        public Specification()
        {
            
        }
        public Specification(Expression<Func<T,bool>> expression)
        {
            Cretria = expression;
        }
    }
}
