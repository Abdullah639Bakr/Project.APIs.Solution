﻿using Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Specifications
{
    public interface ISpecifications<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity , bool>> Criteria { get; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; }

    }
}