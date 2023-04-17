using Microsoft.AspNetCore.Mvc;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Enums;
using Real_Estate.Core.Application.Interfaces.Services;

namespace Real_Estate.Presentation.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;

		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpPost("Authenticate")]
		public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest reqest)
		{
			return Ok(await _accountService.AuthenticateAsync(reqest));
		}

		[HttpPost("AdminUser")]
		public async Task<IActionResult> RegisterAdminAsync(RegisterRequest request)
		{
			var origin = Request.Headers["origin"];
			return Ok(await _accountService.RegisterUserAsync(request, origin, Roles.Admin));
		}

		[HttpPost("RegisterDeveloperUser")]
		public async Task<IActionResult> RegisterWaiterAsync(RegisterRequest request)
		{
			var origin = Request.Headers["origin"];
			return Ok(await _accountService.RegisterUserAsync(request, origin, Roles.Developer));
		}
	}
}
