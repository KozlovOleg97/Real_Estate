using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Real_Estate.Core.Application.DTOs.Account;
using Real_Estate.Core.Application.ViewModels.Users;

namespace Real_Estate.Core.Application.Mappings
{
	public class GeneralProfile : Profile
	{
		public GeneralProfile()
		{
			#region UserProfile

			CreateMap<AuthenticationRequest, LoginViewModel>()
				.ForMember(userProfile => userProfile.HasError,
					option => option.Ignore())
				.ForMember(userProfile => userProfile.Error,
					option => option.Ignore())
				.ReverseMap();

			CreateMap<RegisterRequest, SaveUserViewModel>()
				.ForMember(userProfile => userProfile.File,
					option => option.Ignore())
				.ForMember(userProfile => userProfile.HasError,
					option => option.Ignore())
				.ForMember(userProfile => userProfile.Error,
					option => option.Ignore())
				.ReverseMap();

			CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
				.ForMember(userProfile => userProfile.HasError,
					option => option.Ignore())
				.ForMember(userProfile => userProfile.Error,
					option => option.Ignore())
				.ReverseMap();

			CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
				.ForMember(userProfile => userProfile.HasError,
					option => option.Ignore())
				.ForMember(userProfile => userProfile.Error,
					option => option.Ignore())
				.ReverseMap();

			#endregion
		}
	}
}
