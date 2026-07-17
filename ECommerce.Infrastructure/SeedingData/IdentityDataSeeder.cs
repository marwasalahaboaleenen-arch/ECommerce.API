using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Identity.Data;
using ECommerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.SeedingData
{
    internal class IdentityDataSeeder : IDataSeeder
    {
        private readonly StoreIdentityDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityDataSeeder(StoreIdentityDbContext dbContext, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole>roleManager)
        {
           _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(ct);
                if (PendingMigrations.Any())
                    await _dbContext.Database.MigrateAsync(ct);
                if (!await _roleManager.Roles.AnyAsync())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!await _userManager.Users.AnyAsync(ct))
                {
                    var admin = new ApplicationUser()
                    {
                        DisplayName = "marwasalah",
                        Email = "marwa@gmail.com",
                        UserName = "marwasalahmahmoud",
                        PhoneNumber = "01094996212"

                    };


                    var createResult = await _userManager.CreateAsync(admin, "P@ss0rd");
                    if (createResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(admin, "SuperAdmin");
                    }
                    else
                    {
                        Console.WriteLine("Cannot Assign Role to User !");
                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
