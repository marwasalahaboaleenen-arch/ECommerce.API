using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOs.IdentityDtos;
using ECommerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser>  userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
          this.userManager = userManager;
        }
        public async Task<Result<bool>> CheckPasswordAsync(string email ,string password, CancellationToken ct = default)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return Result<bool>.Fail(Error.NotFound("NotFoundError", "User Not Found"));
            else
                return Result<bool>.Ok(await userManager.CheckPasswordAsync(user, password));

        }

        public async Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.UserName
            };

            var result = await userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(E => new Error(E.Code, E.Description)).ToList();
                return Result<IdentityUserResult>.Fail(errors);
            }
            return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id,user.Email,user.UserName,user.DisplayName));
                
        }

        public async Task<Result<bool>> EmailExistsAsync(string email, CancellationToken ct = default)
        {
            var exists = await userManager.FindByEmailAsync(email) is not null;

            return Result<bool>.Ok(exists);
        }

        public async Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = await userManager.FindByEmailAsync(email);
            if(user == null)
                return Result<IdentityUserResult>.Fail(Error.NotFound("NotFoundError", "User Not Found"));
            else
                return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id,user.DisplayName,user.Email,user.UserName));


        }

        public async Task<Result<IReadOnlyList<string>>> GetUserRoles(string email, CancellationToken ct = default)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return Result<IReadOnlyList<string>>.Fail(Error.NotFound("NotFound", "User NotFound"));
            var roles = await userManager.GetRolesAsync(user);
            return Result<IReadOnlyList<string>>.Ok(roles.ToList());
        }
    }
}
