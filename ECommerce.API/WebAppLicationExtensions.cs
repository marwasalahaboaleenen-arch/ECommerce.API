using ECommerce.Domain.Contracts;

namespace ECommerce.API
{
    public static class WebAppLicationExtensions
    {
  public static async Task<WebApplication>SeedAndMigrationDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            await seeder.SeedDataAsync();
            return app;
        }
    }
}