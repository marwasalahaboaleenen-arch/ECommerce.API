using ECommerce.Application.Contracts;
using ECommerce.Application.DTOs.IdentityDtos;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class AuthenticationController:ApiBaseController
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>>Login(LoginDto loginDto)
            =>ToActionResult(await _authService.LoginAsync(loginDto));


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto,CancellationToken ct =default)
        {
            return ToActionResult(await _authService.RegisterAsync(registerDto, ct));
        }



        //check Email Exists
        [HttpGet("emailexists")]

        public async Task<ActionResult<bool>> CheckEmail([FromQuery]string email, CancellationToken ct = default) 
            =>ToActionResult(await _authService.CheckEmailExistsAsync(email, ct));
    }
}
