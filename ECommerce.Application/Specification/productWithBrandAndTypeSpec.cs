using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Application.Common;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Specification
{
   public class productWithBrandAndTypeSpec :BaseSpecification<Product,int>
    {
        public productWithBrandAndTypeSpec( ProductQueryParams queryParams) :base
            (P=>(!queryParams.BrandId.HasValue||P.BrandId== queryParams.BrandId.Value)&&
            (!queryParams.TypeId.HasValue||P.TypeId==queryParams.TypeId)
            &&(string.IsNullOrWhiteSpace(queryParams.Search)||P.Name.ToLower().Contains(queryParams.Search.ToLower())))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P=>P.ProductType);

            switch(queryParams.Sort)
            {
                case ProductSortOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                    case ProductSortOptions.NameDesc:
                    AddOrderBy(P => P.Name);
                    break;
                    case ProductSortOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                    case ProductSortOptions.PriceDesc:
                    AddOrderBy(P => P.Price);
                    break;
                    default:
                    AddOrderBy(P=>P.Id);
                    break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageSize);
        }
        public productWithBrandAndTypeSpec(int id):base(P=>P.Id==id) 
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

    }
}
