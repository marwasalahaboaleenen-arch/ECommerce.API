using ECommerce.Application.Common;
using ECommerce.Application.DTOs.ProductDtos;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Contracts
{
    public interface IProductService
    {
        Task<Result<IReadOnlyList<ProductDto>>> GetAllProductAsync(CancellationToken ct = default);
    }
}
