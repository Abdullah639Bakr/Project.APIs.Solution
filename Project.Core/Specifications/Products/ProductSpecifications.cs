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

        public ProductSpecifications(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }
            }
            else 
            {
                AddOrderBy(p => p.Name);
            }
            ApplyIncludes();
        }

        private void ApplyIncludes() 
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type);
        }
    }
}
