using ECommerce.Application.Common;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Specification
{
    public class ProductCountSpecification:BaseSpecification<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams) : base
            (P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId.Value) &&
            (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
            && (string.IsNullOrWhiteSpace(queryParams.Search) || P.Name.ToLower().Contains(queryParams.Search.ToLower())))
        {
            
        }
    }
}
