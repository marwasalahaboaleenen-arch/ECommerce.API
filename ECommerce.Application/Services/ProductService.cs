using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOs.ProductDtos;
using ECommerce.Application.Specification;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
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
        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandAsync(CancellationToken ct = default)
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(ct);
            var data = _mapper.Map<IReadOnlyList<BrandDto>>(brands);
            return Result<IReadOnlyList<BrandDto>>.Ok(data);
        }

        public async Task<Result<PaginatedResult<ProductDto>>> GetAllProductAsync(ProductQueryParams queryParams, CancellationToken ct = default)
        {
            var Spec = new productWithBrandAndTypeSpec(queryParams);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(Spec,ct);
            var data = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            var countSpec = new ProductCountSpecification(queryParams);
            var CountOfAllProducts = await _unitOfWork.GetRepository<Product,int>().CountAsync(countSpec);
            var result =  new PaginatedResult<ProductDto>(queryParams.PageIndex, queryParams.PageSize,CountOfAllProducts, data);
            return Result<PaginatedResult<ProductDto>>.Ok(result);
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default)
        {
            var types = _mapper.Map<IReadOnlyList<TypeDto>>(await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(ct));
            return Result<IReadOnlyList<TypeDto>>.Ok(types);
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id , CancellationToken ct = default)
        {
            var Spec = new productWithBrandAndTypeSpec(id);
          var product = await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(Spec, ct);
            if (product == null)
                return Result<ProductDto>.Fail(Error.NotFound("Product.NotFound", $"Product With Id {id}Not Found"));
            return Result<ProductDto>.Ok(_mapper.Map<ProductDto>(product));
        }

      
    }
}
