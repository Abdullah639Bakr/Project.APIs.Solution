﻿using Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Specifications
{
    internal class Specifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes { get; set ; } = new List<Expression<Func<TEntity, object>>> ();

        public Specifications(Expression<Func<TEntity, bool>> expression)
        {
            Criteria= expression;
        }

        public Specifications()
        {
            
        }
    }
}
