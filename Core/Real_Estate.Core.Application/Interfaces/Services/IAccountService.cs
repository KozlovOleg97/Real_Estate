using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.Enums;
using Real_Estate.Core.Application.ViewModels.Admin;
using Real_Estate.Core.Application.ViewModels.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate.Core.Application.Interfaces.Services
{
	public interface IAccountService
	{
        Task<bool> ChangesStatusUser(string id, bool status);
        Task<List<AgentsViewModel>> GetAllUsers();
        Task<List<AgentsViewModel>> GetAllAgents();
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
		Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin, Roles typeOfUser);
		Task SignOutAsync();
		Task<string> ConfirmAccountAsync(string userId, string token);
		Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
		Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task<UpdateAgentUserResponse> UpdateAgentUserByUserNameAsync(UpdateAgentUserRequest request);
        Task<UpdateAgentUserResponse> GetAgentUserByUserNameAsync(string userName);
        Task<HomeAdminViewModel> GetUsersQuantity();
        Task<List<UserViewModel>> GetAllUserViewModels();
    }
}
