using MediatR;
using Microsoft.AspNetCore.Mvc;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Enums;
using Real_Estate.Core.Application.Features.Accounts.Commands.RegisterAdminUser;
using Real_Estate.Core.Application.Features.Accounts.Commands.RegisterDeveloperUser;
using Real_Estate.Core.Application.Features.Accounts.Queries.Authenticate;
using Real_Estate.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Real_Estate.Presentation.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [SwaggerTag("Account Services")]
    public class AccountController : BaseApiController
	{
		private readonly IAccountService _accountService;

		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpPost("Authenticate")]
        [SwaggerOperation(
            Summary = "Login of User",
            Description = "Gets All Properties."
        )]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
		{
            try
            {
                return Ok(await Mediator.Send(new AuthenticateUserQuery
                {
                    Email = request.Email, 
                    Password = request.Password
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

		[HttpPost("RegisterAdminUser")]
        [SwaggerOperation(
            Summary = "Creating Administrator Role",
            Description = "Receives the necessary parameters to create a user with the administrator role."
        )]
        public async Task<IActionResult> RegisterAdminAsync(RegisterAdminUserCommand command)
		{
            return Ok(await Mediator.Send(command));
        }

		[HttpPost("RegisterDeveloperUser")]
        [SwaggerOperation(
            Summary = "Creating Developer Role",
            Description = "Receives the necessary parameters to create a user with the developer role."
        )]
        public async Task<IActionResult> RegisterDeveloperAsync(RegisterDeveloperUserCommand command)
		{
            return Ok(await Mediator.Send(command));
        }
	}
}
