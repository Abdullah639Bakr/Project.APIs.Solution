using Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Specifications.Products
{
    public class ProductSpecifications :BaseSpecifications <Product,int>
    {
        public ProductSpecifications(int id) :base (p=>p.Id==id)
        {
            ApplyIncludes();
        }

        public ProductSpecifications()
        {
            ApplyIncludes();
        }

        private void ApplyIncludes() 
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type);
        }
    }
}
