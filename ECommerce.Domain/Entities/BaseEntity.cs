using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 




namespace ECommerce.Domain.Entities
{
    public abstract class BaseEntity<TKey>
    {
        public IQueryable<object> query;

        public TKey  Id { get; set; }
    }
}
