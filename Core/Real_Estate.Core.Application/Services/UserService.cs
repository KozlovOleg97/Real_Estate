using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Enums;
using Real_Estate.Core.Application.Interfaces.Services;
using Real_Estate.Core.Application.ViewModels.Users;

namespace Real_Estate.Core.Application.Services
{
	public class UserService : IUserService
	{
		private readonly IAccountService _accountService;
		private readonly IMapper _mapper;

		public UserService(IAccountService accountService, IMapper mapper)
		{
			_accountService = accountService;
			_mapper = mapper;
		}
		public async Task<AuthenticationResponse> LoginAsync(LoginViewModel loginViewModel)
		{
			AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(loginViewModel);
			AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
			return userResponse;
		}
		public async Task SignOutAsync()
		{
			await _accountService.SignOutAsync();
		}

		public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel saveUserViewModel, string origin, Roles typeOfUser)
		{
			RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(saveUserViewModel);
			return await _accountService.RegisterUserAsync(registerRequest, origin, typeOfUser);
		}

		public async Task<string> ConfirmEmailAsync(string userId, string token)
		{
			return await _accountService.ConfirmAccountAsync(userId, token);
		}

		public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel forgotPasswordViewModel, string origin)
		{
			ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(forgotPasswordViewModel);
			return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
		}

		public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel resetPasswordViewModel)
		{
			ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(resetPasswordViewModel);
			return await _accountService.ResetPasswordAsync(resetRequest);
		}
	}
}
