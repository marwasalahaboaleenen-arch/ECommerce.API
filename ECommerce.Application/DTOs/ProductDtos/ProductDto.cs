using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs.ProductDtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductUrl { get; set; }
        public string ProductBrand { get; set; }
        public string ProductType { get; set; }
        public decimal Price { get; set; }

    }
}
