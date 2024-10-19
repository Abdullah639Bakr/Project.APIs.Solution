using Project.Core.Dtos.Products;
using Project.Core.Helper;
using Project.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Services.Contract
{
    public interface IProductService
    {
        Task<PaginationResponse<ProductDto>> GetAllProductSAsync(ProductSpecPrames productSpec);
        Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync();
        Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync();
        Task<ProductDto> GetProductById(int id);
    }
}
