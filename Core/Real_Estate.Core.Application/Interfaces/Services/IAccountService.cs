using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.Interfaces.Services
{
	public interface IAccountService
	{
		Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
		Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin, Roles typeOfUser);
	}
}
