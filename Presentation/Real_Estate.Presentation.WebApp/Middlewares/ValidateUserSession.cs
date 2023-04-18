using Microsoft.AspNetCore.Http;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Helpers;

namespace Real_Estate.Presentation.WebApp.Middlewares
{
	public class ValidateUserSession
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public bool HasUser()
		{
			AuthenticationResponse userViewModel =
				_httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

			if (userViewModel == null)
			{
				return false;
			}

			return true;
		}
	}
}
