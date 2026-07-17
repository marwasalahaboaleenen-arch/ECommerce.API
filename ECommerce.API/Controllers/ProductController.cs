using ECommerce.API.Attributes;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOs.ProductDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class ProductController :ApiBaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
         _productService = productService;
            
        }
        [HttpGet]
        [RedisCache(90)]
        public async Task<ActionResult<PaginatedResult<ProductDto>>>GetAllProducts([FromQuery]ProductQueryParams queryParams, CancellationToken ct)
        {
            var result = await _productService.GetAllProductAsync(queryParams, ct);
            return ToActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id, CancellationToken ct)
        {
            var result = await _productService.GetProductByIdAsync(id, ct); 
            return ToActionResult(result);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct)
        {
            var brands = await _productService.GetAllBrandAsync(ct);
            return ToActionResult(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct)
        {
            var types = await _productService.GetAllTypesAsync(ct);
            return ToActionResult(types);
        }
    }
}
