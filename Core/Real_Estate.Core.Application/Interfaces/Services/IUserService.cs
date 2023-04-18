using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Enums;
using Real_Estate.Core.Application.ViewModels.Users;

namespace Real_Estate.Core.Application.Interfaces.Services
{
	public interface IUserService
	{
		Task<AuthenticationResponse> LoginAsync(LoginViewModel loginViewModel);
		Task SignOutAsync();
		Task<RegisterResponse> RegisterAsync(SaveUserViewModel saveUserViewModel, 
			string origin, Roles typeOfUser);
		Task<string> ConfirmEmailAsync(string userId, string token);
		Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel forgotPasswordViewModel, 
			string origin);
		Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel resetPasswordViewModel);
	}
}
