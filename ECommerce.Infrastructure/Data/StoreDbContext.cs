using ECommerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Data
{
    public class StoreDbContext(DbContextOptions<StoreDbContext>Options) : DbContext(Options)
    {
      public DbSet<Product> Products { get; set; }
     public DbSet<ProductBrand>ProductBrands { get; set; }
     public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);

        }


    }
}
