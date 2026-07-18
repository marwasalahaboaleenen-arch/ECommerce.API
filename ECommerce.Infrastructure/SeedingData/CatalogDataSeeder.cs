using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.Orders;
using ECommerce.Domain.Entities.Products;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.SeedingData
{
    public class CatalogDataSeeder(
    StoreDbContext dbContext,
    ILogger<CatalogDataSeeder> logger) : IDataSeeder
    {
 	        
		   public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {

                var pendingMigration = await dbContext.Database.GetPendingMigrationsAsync(ct);
                if (pendingMigration.Any())
                    await dbContext.Database.MigrateAsync();



                var rootPath = Path.Combine(AppContext.BaseDirectory, "DataSeed");
                await SeedDataIfEmptyAsync<ProductBrand, int>(rootPath, "brands.json", ct);
                await SeedDataIfEmptyAsync<ProductType, int>(rootPath, "types.json", ct);
                await SeedDataIfEmptyAsync<Product, int>(rootPath, "products.json", ct);
                await SeedDataIfEmptyAsync<DeliveryMethod, int>(rootPath, "delivery.json", ct);

                var result = await dbContext.SaveChangesAsync(ct);
                if (result > 0)
                {
                    logger.LogInformation($"Data Seeded Successfully,{result}rows affected");
                }
                else
                    logger.LogInformation("Failed to seed Data");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
	
       


        private async Task SeedDataIfEmptyAsync<T, TKey>(string rootPath, string fileName, CancellationToken ct) where T : BaseEntity<TKey>
        {
            if (await dbContext.Set<T>().AnyAsync())
            { return; }
         var filePath = Path.Combine(rootPath, fileName);
            if (!File.Exists(filePath))
            {
                return;
            }
          using var fileStream = File.OpenRead(filePath);
            var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream);
            if(items?.Any() ?? false)
                dbContext.Set<T>().AddRange(items);
        }

    }
}
