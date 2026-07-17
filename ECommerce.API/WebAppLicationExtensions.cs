using ECommerce.Domain.Contracts;

namespace ECommerce.API
{
    public static class WebAppLicationExtensions
    {
  public static async Task<WebApplication>SeedAndMigrationDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            var IdentitySeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Identity");
            await seeder.SeedDataAsync();
            await IdentitySeeder.SeedDataAsync();
            return app;
        }
    }
}