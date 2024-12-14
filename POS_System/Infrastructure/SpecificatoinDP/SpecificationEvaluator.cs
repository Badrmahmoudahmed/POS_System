using Microsoft.EntityFrameworkCore;
using POS_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.Infrastructure.SpecificatoinDP
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> Currquery,Specification<T> spec)
        {
            var query = Currquery;

            if (spec.Cretria is not null)
                query = query.Where(spec.Cretria);

            if (query is not null)
                query = spec.Includes.Aggregate(query, (curr, expression) => curr.Include(expression));

            return query;
        }
    }
}
