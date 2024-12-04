using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entites.Common;
using Talabat.core.Specifications;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Talabat.Infrastructure
{
    public static class SpacificationEvaluator<T>where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T>  inputQuery , ISpecification<T> spec)
        {
            var Query = inputQuery;
            if (spec.Criteria!=null)
                Query=Query.Where(spec.Criteria);
            if (spec.OrderBy != null)
            {
                Query=Query.OrderBy(spec.OrderBy);  

            }
            else if (spec.OrderByDesc != null)
            {
                Query = Query.OrderByDescending(spec.OrderByDesc);

            }
            if (spec.IsPaginationEnabled)
                Query = Query.Skip(spec.Skip).Take(spec.Take);

           Query = spec.Includes.Aggregate(Query, (current, include) => current.Include(include));


            return Query;


        }


    }
}
