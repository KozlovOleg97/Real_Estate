using Microsoft.AspNetCore.Mvc;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Enums;
using Real_Estate.Core.Application.Helpers;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Application.ViewModels.Users;
using Real_Estate.Presentation.WebApp.Middlewares;

namespace Real_Estate.Presentation.WebApp.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserService _userService;
		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[ServiceFilter(typeof(LoginAuthorize))]
		public IActionResult Index()
		{
			return View(new LoginViewModel());
		}

		[ServiceFilter(typeof(LoginAuthorize))]
		[HttpPost]
		public async Task<IActionResult> Index(LoginViewModel loginViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(loginViewModel);
			}

			AuthenticationResponse userViewModel = await _userService.LoginAsync(loginViewModel);
			if (userViewModel == null && userViewModel.HasError != true)
			{
				HttpContext.Session.Set<AuthenticationResponse>("user", userViewModel);

				if (userViewModel.Roles.ToString() == Roles.Client.ToString())
				{
					return RedirectToRoute(new
					{
						controller = "Agent",
						action = "Index"
					});
				}

				else if (userViewModel.Roles.ToString() == Roles.Agent.ToString())
				{
					return RedirectToRoute(new
					{
						controller = "Agent",
						action = "Index"
					});
				}

				else if (userViewModel.Roles.ToString() == Roles.Developer.ToString())
				{
					return RedirectToRoute(new
					{
						controller = "User",
						action = "AccessDenied"
					});
				}

				else
				{
					return RedirectToRoute(new
					{
						controller = "Admin",
						action = "Index"
					});
				}
			}

			else
			{
				loginViewModel.HasError = userViewModel.HasError;
				loginViewModel.Error = userViewModel.Error;
				return View(loginViewModel);
			}
		}

		public async Task<IActionResult> Logout()
		{
			await _userService.SignOutAsync();
			HttpContext.Session.Remove("user");
			return RedirectToRoute(new
			{
				controller = "Home",
				action = "Index"
			});
		}

		[ServiceFilter(typeof(LoginAuthorize))]
		public IActionResult Register()
		{
			List<string> userTypes = new List<string>();
			userTypes.Add(Roles.Client.ToString());
			userTypes.Add(Roles.Agent.ToString());
			ViewBag.UserTypes = userTypes;
			return View(new SaveUserViewModel());
		}

		[ServiceFilter(typeof(LoginAuthorize))]
		[HttpPost]
		public async Task<IActionResult> Register(SaveUserViewModel saveUserViewModel, string role)
		{
			if (!ModelState.IsValid)
			{
				List<string> userTypes = new List<string>();
				userTypes.Add(Roles.Client.ToString());
				userTypes.Add(Roles.Agent.ToString());
				ViewBag.UserTypes = userTypes;
				return View(saveUserViewModel);
			}

			saveUserViewModel.ImagePath = UploadImagesHelper.UploadUserImage(saveUserViewModel.File,
				saveUserViewModel.UserName);

			var origin = Request.Headers["origin"];
			RegisterResponse response = new RegisterResponse();

			if (role == Roles.Client.ToString())
			{
				response = await _userService.RegisterAsync(saveUserViewModel, origin, Roles.Client);
			}
			else if (role == Roles.Agent.ToString())
			{
				response = await _userService.RegisterAsync(saveUserViewModel, origin, Roles.Agent);
			}

			if (response.HasError)
			{
				saveUserViewModel.HasError = response.HasError;
				saveUserViewModel.Error = response.Error;

				List<string> userTypes = new List<string>();
				userTypes.Add(Roles.Client.ToString());
				userTypes.Add(Roles.Agent.ToString());
				ViewBag.UserTypes = userTypes;

				return View(saveUserViewModel);
			}

			return RedirectToRoute(new
			{
				controller = "User",
				action = "Index"
			});
		}

		[ServiceFilter(typeof(LoginAuthorize))]
		public async Task<IActionResult> ConfirmEmail(string userId, string token)
		{
			string response = await _userService.ConfirmEmailAsync(userId, token);
			return View("ConfirmEmail", response);
		}

		[ServiceFilter(typeof(LoginAuthorize))]
		public async Task<IActionResult> ForgotPassword()
		{
			return View(new ForgotPasswordViewModel());
		}

		[ServiceFilter(typeof(LoginAuthorize))]
		[HttpPost]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(forgotPasswordViewModel);
			}

			var origin = Request.Headers["origin"];
			ForgotPasswordResponse response = await _userService.ForgotPasswordAsync(forgotPasswordViewModel, origin);
			if (response.HasError)
			{
				forgotPasswordViewModel.HasError = response.HasError;
				forgotPasswordViewModel.Error = response.Error;
				return View(forgotPasswordViewModel);
			}
			return RedirectToRoute(new { controller = "User", action = "Index" });
		}

		[ServiceFilter(typeof(LoginAuthorize))]
		public async Task<IActionResult> ResetPassword(string token)
		{
			return View(new ResetPasswordViewModel { Token = token });
		}

		[ServiceFilter(typeof(LoginAuthorize))]
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(resetPasswordViewModel);
			}

			ResetPasswordResponse response = await _userService.ResetPasswordAsync(resetPasswordViewModel);
			if (response.HasError)
			{
				resetPasswordViewModel.HasError = response.HasError;
				resetPasswordViewModel.Error = response.Error;
				return View(resetPasswordViewModel);
			}

			return RedirectToRoute(new
			{
				controller = "User", 
				action = "Index"
			});
		}

		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
