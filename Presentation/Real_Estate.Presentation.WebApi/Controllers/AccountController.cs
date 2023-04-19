using MediatR;
using Microsoft.AspNetCore.Mvc;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Enums;
using Real_Estate.Core.Application.Features.Accounts.Commands.RegisterAdminUser;
using Real_Estate.Core.Application.Features.Accounts.Commands.RegisterDeveloperUser;
using Real_Estate.Core.Application.Features.Accounts.Queries.Authenticate;
using Real_Estate.Core.Application.Interfaces.Services;

namespace Real_Estate.Presentation.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : BaseApiController
	{
		private readonly IAccountService _accountService;

		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpPost("Authenticate")]
		public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
		{
            return Ok(await Mediator.Send(new AuthenticateUserQuery
            {
                Email = request.Email, 
                Password = request.Password
            }));
        }

		[HttpPost("RegisterAdminUser")]
		public async Task<IActionResult> RegisterAdminAsync(RegisterAdminUserCommand command)
		{
            return Ok(await Mediator.Send(command));
        }

		[HttpPost("RegisterDeveloperUser")]
		public async Task<IActionResult> RegisterDeveloperAsync(RegisterDeveloperUserCommand command)
		{
            return Ok(await Mediator.Send(command));
        }
	}
}
