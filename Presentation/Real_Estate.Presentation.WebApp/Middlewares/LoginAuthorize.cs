using Microsoft.AspNetCore.Mvc.Filters;
using Real_Estate.Presentation.WebApp.Controllers;

namespace Real_Estate.Presentation.WebApp.Middlewares
{
	public class LoginAuthorize : IAsyncActionFilter
	{
		private readonly ValidateUserSession _userSession;

		public LoginAuthorize(ValidateUserSession userSession)
		{
			_userSession = userSession;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, 
            ActionExecutionDelegate next)
		{
			if (_userSession.HasUser())
			{
				var controller = (HomeController)context.Controller;
				context.Result = controller.RedirectToAction("Index", "Home");
			}
			else
			{
				await next();
			}
		}
	}
}
