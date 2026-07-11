using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs.BasketDtos
{
   public class BasketDto
    {
     public int Id { get; set; }
        public ICollection<BasketItemsDto> Items {  get; set; }


    }
}
