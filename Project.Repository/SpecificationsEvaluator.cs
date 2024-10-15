using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Project.Core.Entities;
using Project.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public static class SpecificationsEvaluator<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> QetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec) 
        {
            var query = inputQuery;
            if (spec.Criteria is not null) 
            { 
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));

            return query;
        }
    }
}
