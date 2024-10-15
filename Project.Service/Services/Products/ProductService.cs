using AutoMapper;
using Project.Core;
using Project.Core.Dtos.Products;
using Project.Core.Entities;
using Project.Core.Services.Contract;
using Project.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ProductDto>> GetAllProductSAsync()
        {

            var spec = new ProductSpecifications();
            var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec);
            var mappedProducts = _mapper.Map<IEnumerable<ProductDto>>(products);

            return mappedProducts;
            //return _mapper.Map<IEnumerable<ProductDto>>(await _unitOfWork.Repository<Product, int>().GetAllAsync()) ;
        }

        public async Task<ProductDto> GetProductById(int id)
        {

            var spec = new ProductSpecifications(id);
            return _mapper.Map<ProductDto>(await _unitOfWork.Repository<Product, int>().GetWithSpecAsync(spec));

            //var product = await _unitOfWork.Repository<Product, int>().GetAsync(id);
            //var productMapper = _mapper.Map<ProductDto>(product);
            //return productMapper;
        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        {
             return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductType,int>().GetAllAsync());
          
        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            var brands = _unitOfWork.Repository<ProductBrand,int>().GetAllAsync();
            var tbdMapper = _mapper.Map<IEnumerable<TypeBrandDto>>(brands);
            return tbdMapper;
           //return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync());
        }

      




    }
}
