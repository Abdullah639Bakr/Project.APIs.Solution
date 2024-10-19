using Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Specifications.Products
{
    public class ProductWithCountSpecifications : BaseSpecifications<Product,int>
    {
        public ProductWithCountSpecifications(ProductSpecPrames productSpec) : 
            base(
                 p =>
                     (string.IsNullOrEmpty(productSpec.Search) || p.Name.ToLower().Contains(productSpec.Search))
                     &&
                     (!productSpec.BrandId.HasValue || productSpec.BrandId == p.BrandId)
                     &&
                     (!productSpec.TypeId.HasValue || productSpec.TypeId == p.TypeId)
                )
        {
           
        }

    }
}
