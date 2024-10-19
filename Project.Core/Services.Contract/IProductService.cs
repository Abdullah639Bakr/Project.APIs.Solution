using Project.Core.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Services.Contract
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductSAsync(string? sort ,int? brandId, int? typeId , int? pageSize , int? pageIndex);
        Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync();
        Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync();
        Task<ProductDto> GetProductById(int id);
    }
}
