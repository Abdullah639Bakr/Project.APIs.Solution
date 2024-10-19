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

        public ProductSpecifications(string? sort , int? brandId, int? typeId, int pageSize, int pageIndex) : base(
            p=>
            ( !brandId.HasValue || brandId == p.BrandId)
            &&
            ( !typeId.HasValue || typeId == p.TypeId)
            )
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
            ApplyPagination(pageSize*(pageIndex-1),pageSize);
        }

        private void ApplyIncludes() 
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type);
        }
    }
}
